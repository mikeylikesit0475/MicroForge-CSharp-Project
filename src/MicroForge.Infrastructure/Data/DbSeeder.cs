using MicroForge.Domain.Entities;
using MicroForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroForge.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(MicroForgeDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Roles.AnyAsync())
            return;

        var roles = new List<Role>
        {
            new() { Name = "Admin", Description = "Full system access", Permissions = "*" },
            new() { Name = "Engineer", Description = "Engineering access", Permissions = "read,write,agent" },
            new() { Name = "Operator", Description = "Operator access", Permissions = "read,write" },
            new() { Name = "Viewer", Description = "Read-only access", Permissions = "read" }
        };
        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();

        var lines = new List<ProductionLine>
        {
            new() { Name = "Line Alpha", Location = "Building A - Floor 1", LineType = "Photolithography", IsActive = true },
            new() { Name = "Line Beta", Location = "Building A - Floor 2", LineType = "Etching", IsActive = true },
            new() { Name = "Line Gamma", Location = "Building B - Floor 1", LineType = "Deposition", IsActive = true }
        };
        await context.ProductionLines.AddRangeAsync(lines);
        await context.SaveChangesAsync();

        var adminRole = roles[0];
        var engineerRole = roles[1];
        var operatorRole = roles[2];

        var admin = new User
        {
            Username = "admin",
            Email = "admin@microforge.io",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123", workFactor: 12),
            FirstName = "System",
            LastName = "Administrator",
            IsActive = true
        };
        var engineer = new User
        {
            Username = "engineer01",
            Email = "engineer01@microforge.io",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("engineer123", workFactor: 12),
            FirstName = "Jane",
            LastName = "Engineer",
            IsActive = true
        };
        var operatorUser = new User
        {
            Username = "operator01",
            Email = "operator01@microforge.io",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("operator123", workFactor: 12),
            FirstName = "John",
            LastName = "Operator",
            IsActive = true
        };
        await context.Users.AddRangeAsync(admin, engineer, operatorUser);
        await context.SaveChangesAsync();

        await context.UserRoles.AddRangeAsync(
            new UserRole { UserId = admin.Id, RoleId = adminRole.Id },
            new UserRole { UserId = engineer.Id, RoleId = engineerRole.Id },
            new UserRole { UserId = operatorUser.Id, RoleId = operatorRole.Id }
        );

        await context.UserProductionLines.AddRangeAsync(
            new UserProductionLine { UserId = operatorUser.Id, ProductionLineId = lines[0].Id },
            new UserProductionLine { UserId = operatorUser.Id, ProductionLineId = lines[1].Id }
        );

        await context.ProductionRuns.AddRangeAsync(
            new ProductionRun
            {
                ProductionLineId = lines[0].Id, BatchNumber = "BATCH-001",
                StartTime = DateTime.UtcNow.AddHours(-2), Status = "Running",
                TargetYield = 95.5m, ActualYield = 0
            },
            new ProductionRun
            {
                ProductionLineId = lines[1].Id, BatchNumber = "BATCH-002",
                StartTime = DateTime.UtcNow.AddHours(-4), Status = "Completed",
                EndTime = DateTime.UtcNow.AddHours(-1), TargetYield = 92.0m, ActualYield = 91.3m
            },
            new ProductionRun
            {
                ProductionLineId = lines[0].Id, BatchNumber = "BATCH-003",
                StartTime = DateTime.UtcNow.AddDays(-1), Status = "Completed",
                EndTime = DateTime.UtcNow.AddDays(-1).AddHours(3), TargetYield = 95.5m, ActualYield = 94.8m
            },
            new ProductionRun
            {
                ProductionLineId = lines[1].Id, BatchNumber = "BATCH-004",
                StartTime = DateTime.UtcNow.AddDays(-2), Status = "Completed",
                EndTime = DateTime.UtcNow.AddDays(-2).AddHours(4), TargetYield = 92.0m, ActualYield = 90.1m
            },
            new ProductionRun
            {
                ProductionLineId = lines[2].Id, BatchNumber = "BATCH-005",
                StartTime = DateTime.UtcNow.AddDays(-3), Status = "Completed",
                EndTime = DateTime.UtcNow.AddDays(-3).AddHours(5), TargetYield = 88.0m, ActualYield = 87.2m
            },
            new ProductionRun
            {
                ProductionLineId = lines[2].Id, BatchNumber = "BATCH-006",
                StartTime = DateTime.UtcNow.AddHours(-1), Status = "Running",
                TargetYield = 88.0m, ActualYield = 0
            }
        );

        await context.SaveChangesAsync();

        // Seed sensor readings so tools return data even without the simulator running
        var sensorReadings = new List<SensorReading>();
        var sensorTypes = new[] { "Temperature", "Pressure", "Vibration", "Humidity" };
        var sensorUnits = new[] { "°C", "bar", "mm/s", "%" };
        var sensorBases = new[] { 25m, 2.0m, 3.0m, 45m };
        var sensorRanges = new[] { 5m, 0.5m, 1.0m, 5m };
        var rng = new Random(42);
        foreach (var line in lines)
        {
            for (var i = 0; i < 20; i++)
            {
                for (var s = 0; s < sensorTypes.Length; s++)
                {
                    var value = sensorBases[s] + (decimal)(rng.NextDouble() * (double)sensorRanges[s] - (double)sensorRanges[s] / 2);
                    var isAnomaly = i == 5 && s == 0; // one temperature anomaly per line
                    sensorReadings.Add(new SensorReading
                    {
                        ProductionLineId = line.Id,
                        SensorType = sensorTypes[s],
                        Value = Math.Round(value, 2),
                        Unit = sensorUnits[s],
                        Timestamp = DateTime.UtcNow.AddMinutes(-i * 5),
                        IsAnomaly = isAnomaly,
                        AnomalyType = isAnomaly ? "HighThreshold" : null
                    });
                }
            }
        }
        await context.SensorReadings.AddRangeAsync(sensorReadings);

        // Seed a few alerts
        await context.Alerts.AddRangeAsync(
            new Alert
            {
                ProductionLineId = lines[0].Id, AlertType = "HighThreshold", Severity = "Critical",
                Title = "Temperature Anomaly on Line Alpha", Message = "Temperature exceeded 90°C threshold",
                Source = "SensorSimulator", IsAcknowledged = false, CreatedAt = DateTime.UtcNow.AddMinutes(-25)
            },
            new Alert
            {
                ProductionLineId = lines[1].Id, AlertType = "SPC_Violation", Severity = "Warning",
                Title = "SPC Rule Violation on Line Beta (Pressure)", Message = "2 of 3 consecutive points beyond 2 sigma",
                Source = "SensorSimulator", IsAcknowledged = false, CreatedAt = DateTime.UtcNow.AddMinutes(-15)
            },
            new Alert
            {
                ProductionLineId = lines[2].Id, AlertType = "LowThreshold", Severity = "Warning",
                Title = "Pressure Drop on Line Gamma", Message = "Pressure below 0.5 bar threshold",
                Source = "SensorSimulator", IsAcknowledged = true, CreatedAt = DateTime.UtcNow.AddHours(-2),
                AcknowledgedAt = DateTime.UtcNow.AddHours(-1)
            }
        );

        // Seed a few defect records
        await context.DefectRecords.AddRangeAsync(
            new DefectRecord
            {
                ProductionRunId = 2, DefectType = "Scratch", Severity = "Major",
                Description = "Visible scratch on wafer edge detected during inspection",
                DetectedAt = DateTime.UtcNow.AddHours(-3), DetectedByUserId = admin.Id,
                ClassifiedType = "Mechanical", CoordinateX = 142.5m, CoordinateY = 8.3m
            },
            new DefectRecord
            {
                ProductionRunId = 2, DefectType = "Contamination", Severity = "Critical",
                Description = "Particle contamination detected on wafer surface",
                DetectedAt = DateTime.UtcNow.AddHours(-3), DetectedByUserId = engineer.Id,
                ClassifiedType = "Particulate", CoordinateX = 67.2m, CoordinateY = 91.1m
            },
            new DefectRecord
            {
                ProductionRunId = 3, DefectType = "Crack", Severity = "Minor",
                Description = "Small crack near wafer center",
                DetectedAt = DateTime.UtcNow.AddDays(-1), DetectedByUserId = engineer.Id,
                ClassifiedType = "Mechanical", CoordinateX = 75.0m, CoordinateY = 75.0m
            }
        );

        await context.SaveChangesAsync();

        // ─── Seed realistic fab route steps (DRAM/NAND flow) ───────────────
        var routeSteps = new List<RouteStep>
        {
            new() { StepName = "WAFER_PREP", SequenceOrder = 1, Description = "Wafer cleaning and surface preparation" },
            new() { StepName = "OXIDATION", SequenceOrder = 2, Description = "Thermal oxidation — grow gate SiO2 layer" },
            new() { StepName = "CVD_DEPOSITION", SequenceOrder = 3, Description = "Chemical vapor deposition — dielectric/nitride films" },
            new() { StepName = "PHOTOLITHO_1", SequenceOrder = 4, Description = "Photolithography Layer 1 — active area patterning" },
            new() { StepName = "ETCH_1", SequenceOrder = 5, Description = "Plasma etch — active area definition" },
            new() { StepName = "ION_IMPLANT_1", SequenceOrder = 6, Description = "Ion implantation — well/channel doping (B, P, As)" },
            new() { StepName = "ANNEAL_1", SequenceOrder = 7, Description = "Rapid thermal anneal — dopant activation and damage repair" },
            new() { StepName = "CMP_1", SequenceOrder = 8, Description = "Chemical mechanical polishing — surface planarization" },
            new() { StepName = "PHOTOLITHO_2", SequenceOrder = 9, Description = "Photolithography Layer 2 — gate electrode patterning" },
            new() { StepName = "ETCH_2", SequenceOrder = 10, Description = "Plasma etch — gate stack definition" },
            new() { StepName = "ALD_DEPOSITION", SequenceOrder = 11, Description = "Atomic layer deposition — high-k dielectric gate" },
            new() { StepName = "METAL_DEPOSITION", SequenceOrder = 12, Description = "PVD/sputtering — gate metal electrode (TiN/TaN)" },
            new() { StepName = "CMP_2", SequenceOrder = 13, Description = "Chemical mechanical polishing — gate planarization" },
            new() { StepName = "PHOTOLITHO_3", SequenceOrder = 14, Description = "Photolithography Layer 3 — contact/via patterning" },
            new() { StepName = "ETCH_3", SequenceOrder = 15, Description = "Plasma etch — contact/via etch through dielectric" },
            new() { StepName = "METAL_1", SequenceOrder = 16, Description = "Metallization — first metal interconnect layer (M1)" },
            new() { StepName = "CMP_3", SequenceOrder = 17, Description = "Chemical mechanical polishing — M1 planarization" },
            new() { StepName = "PHOTOLITHO_4", SequenceOrder = 18, Description = "Photolithography Layer 4 — via/interconnect patterning" },
            new() { StepName = "ETCH_4", SequenceOrder = 19, Description = "Plasma etch — via etch for M2 interconnect" },
            new() { StepName = "METAL_2", SequenceOrder = 20, Description = "Metallization — second metal interconnect layer (M2)" },
            new() { StepName = "CMP_4", SequenceOrder = 21, Description = "Chemical mechanical polishing — M2 planarization" },
            new() { StepName = "METROLOGY_CD", SequenceOrder = 22, Description = "Critical dimension (CD) measurement and overlay check" },
            new() { StepName = "DEFECT_INSPECTION", SequenceOrder = 23, Description = "Defect inspection — optical and e-beam review" },
            new() { StepName = "WAFER_SORT", SequenceOrder = 24, Description = "Wafer probe test — die-level electrical test and yield mapping" },
            new() { StepName = "FINAL_INSPECTION", SequenceOrder = 25, Description = "Final visual inspection and quality sign-off" },
            new() { StepName = "COMPLETED", SequenceOrder = 26, Description = "Lot complete — ready for assembly and packaging" }
        };
        await context.RouteSteps.AddRangeAsync(routeSteps);
        await context.SaveChangesAsync();

        // ─── Seed WIP lots with wafers ────────────────────────────────────
        var lot1 = new Lot
        {
            LotNumber = "LOT-2026-001",
            RecipeName = "DRAM-16Gb-v3",
            ActiveRouteStepId = routeSteps[3].Id, // PHOTOLITHO_1
            Status = LotStatus.Active,
            CreatedAt = DateTime.UtcNow.AddHours(-6)
        };
        var lot2 = new Lot
        {
            LotNumber = "LOT-2026-002",
            RecipeName = "NAND-256Gb-v2",
            ActiveRouteStepId = routeSteps[7].Id, // CMP_1
            Status = LotStatus.Active,
            CreatedAt = DateTime.UtcNow.AddHours(-12)
        };
        var lot3 = new Lot
        {
            LotNumber = "LOT-2026-003",
            RecipeName = "DRAM-8Gb-v5",
            ActiveRouteStepId = routeSteps[14].Id, // PHOTOLITHO_3
            Status = LotStatus.Hold,
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        };
        await context.Lots.AddRangeAsync(lot1, lot2, lot3);
        await context.SaveChangesAsync();

        // Seed wafers for each lot (25 wafers per lot)
        var wafers = new List<Wafer>();
        foreach (var lot in new[] { lot1, lot2, lot3 })
        {
            for (var slot = 1; slot <= 25; slot++)
            {
                wafers.Add(new Wafer
                {
                    WaferId = $"{lot.LotNumber}-W{slot:D2}",
                    LotId = lot.Id,
                    SlotPosition = slot,
                    Status = slot == 13 && lot.Id == lot3.Id ? "Quarantined" : "Good"
                });
            }
        }
        await context.Wafers.AddRangeAsync(wafers);

        // Seed movement logs for lot1 (entered PHOTOLITHO_1)
        await context.LotMovementLogs.AddRangeAsync(
            new LotMovementLog
            {
                LotId = lot1.Id, RouteStepId = routeSteps[0].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-6), ExitedAt = DateTime.UtcNow.AddHours(-5),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot1.Id, RouteStepId = routeSteps[1].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-5), ExitedAt = DateTime.UtcNow.AddHours(-4),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot1.Id, RouteStepId = routeSteps[2].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-4), ExitedAt = DateTime.UtcNow.AddHours(-3),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot1.Id, RouteStepId = routeSteps[3].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-3),
                OperatorId = operatorUser.Id
            }
        );

        // Seed movement logs for lot2 (entered CMP_1)
        await context.LotMovementLogs.AddRangeAsync(
            new LotMovementLog
            {
                LotId = lot2.Id, RouteStepId = routeSteps[0].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-12), ExitedAt = DateTime.UtcNow.AddHours(-11),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot2.Id, RouteStepId = routeSteps[1].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-11), ExitedAt = DateTime.UtcNow.AddHours(-10),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot2.Id, RouteStepId = routeSteps[2].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-10), ExitedAt = DateTime.UtcNow.AddHours(-9),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot2.Id, RouteStepId = routeSteps[3].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-9), ExitedAt = DateTime.UtcNow.AddHours(-8),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot2.Id, RouteStepId = routeSteps[4].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-8), ExitedAt = DateTime.UtcNow.AddHours(-7),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot2.Id, RouteStepId = routeSteps[5].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-7), ExitedAt = DateTime.UtcNow.AddHours(-6),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot2.Id, RouteStepId = routeSteps[6].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-6), ExitedAt = DateTime.UtcNow.AddHours(-5),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot2.Id, RouteStepId = routeSteps[7].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-5),
                OperatorId = operatorUser.Id
            }
        );

        // Seed movement logs for lot3 (entered PHOTOLITHO_3, now on hold)
        await context.LotMovementLogs.AddRangeAsync(
            new LotMovementLog
            {
                LotId = lot3.Id, RouteStepId = routeSteps[0].Id,
                EnteredAt = DateTime.UtcNow.AddDays(-1), ExitedAt = DateTime.UtcNow.AddHours(-23),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot3.Id, RouteStepId = routeSteps[1].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-23), ExitedAt = DateTime.UtcNow.AddHours(-22),
                OperatorId = operatorUser.Id
            },
            new LotMovementLog
            {
                LotId = lot3.Id, RouteStepId = routeSteps[14].Id,
                EnteredAt = DateTime.UtcNow.AddHours(-22),
                OperatorId = operatorUser.Id
            }
        );

        // ─── Seed tool qualifications ──────────────────────────────────────
        var quals = new List<ToolQualification>
        {
            new() { ProductionLineId = lines[0].Id, RecipeName = "DRAM-16Gb-v3", IsQualified = true, QualifiedAt = DateTime.UtcNow.AddDays(-7) },
            new() { ProductionLineId = lines[0].Id, RecipeName = "NAND-256Gb-v2", IsQualified = false, QualifiedAt = DateTime.UtcNow.AddDays(-3) },
            new() { ProductionLineId = lines[1].Id, RecipeName = "DRAM-16Gb-v3", IsQualified = true, QualifiedAt = DateTime.UtcNow.AddDays(-5) },
            new() { ProductionLineId = lines[1].Id, RecipeName = "DRAM-8Gb-v5", IsQualified = true, QualifiedAt = DateTime.UtcNow.AddDays(-2) },
            new() { ProductionLineId = lines[2].Id, RecipeName = "NAND-256Gb-v2", IsQualified = true, QualifiedAt = DateTime.UtcNow.AddDays(-4) },
            new() { ProductionLineId = lines[2].Id, RecipeName = "DRAM-16Gb-v3", IsQualified = true, QualifiedAt = DateTime.UtcNow.AddDays(-1) }
        };
        await context.ToolQualifications.AddRangeAsync(quals);

        // ─── Seed work instructions for key steps ──────────────────────────
        var workInstructions = new List<WorkInstruction>
        {
            new() { RouteStepId = routeSteps[0].Id, TaskDescription = "Verify wafer cassette ID matches lot traveler", IsMandatory = true },
            new() { RouteStepId = routeSteps[0].Id, TaskDescription = "Run SC1 clean recipe and log particle count", IsMandatory = true },
            new() { RouteStepId = routeSteps[0].Id, TaskDescription = "Record pre-process wafer inspection results", IsMandatory = false },

            new() { RouteStepId = routeSteps[3].Id, TaskDescription = "Verify reticle ID matches layer specification", IsMandatory = true },
            new() { RouteStepId = routeSteps[3].Id, TaskDescription = "Check exposure dose and focus calibration", IsMandatory = true },
            new() { RouteStepId = routeSteps[3].Id, TaskDescription = "Run focus-exposure matrix (FEM) on test wafer", IsMandatory = false },

            new() { RouteStepId = routeSteps[4].Id, TaskDescription = "Verify etch recipe matches process spec", IsMandatory = true },
            new() { RouteStepId = routeSteps[4].Id, TaskDescription = "Check endpoint detector calibration", IsMandatory = true },
            new() { RouteStepId = routeSteps[4].Id, TaskDescription = "Log post-etch CD measurement", IsMandatory = false },

            new() { RouteStepId = routeSteps[5].Id, TaskDescription = "Verify implant species and dose match recipe", IsMandatory = true },
            new() { RouteStepId = routeSteps[5].Id, TaskDescription = "Check beam current and uniformity", IsMandatory = true },

            new() { RouteStepId = routeSteps[7].Id, TaskDescription = "Verify CMP slurry type and flow rate", IsMandatory = true },
            new() { RouteStepId = routeSteps[7].Id, TaskDescription = "Check pad conditioning status", IsMandatory = true },
            new() { RouteStepId = routeSteps[7].Id, TaskDescription = "Log post-CMP thickness uniformity", IsMandatory = false },

            new() { RouteStepId = routeSteps[21].Id, TaskDescription = "Measure CD at 5 sites per wafer", IsMandatory = true },
            new() { RouteStepId = routeSteps[21].Id, TaskDescription = "Check overlay registration error", IsMandatory = true },

            new() { RouteStepId = routeSteps[22].Id, TaskDescription = "Run full wafer defect scan", IsMandatory = true },
            new() { RouteStepId = routeSteps[22].Id, TaskDescription = "Review and classify defects on SEM", IsMandatory = false },

            new() { RouteStepId = routeSteps[23].Id, TaskDescription = "Load wafer probe card and verify contact", IsMandatory = true },
            new() { RouteStepId = routeSteps[23].Id, TaskDescription = "Run electrical test program and log bin results", IsMandatory = true },
            new() { RouteStepId = routeSteps[23].Id, TaskDescription = "Generate wafer yield map", IsMandatory = false }
        };
        await context.WorkInstructions.AddRangeAsync(workInstructions);

        // ─── Seed tool maintenance logs ───────────────────────────────────
        await context.ToolMaintenanceLogs.AddRangeAsync(
            new ToolMaintenanceLog
            {
                ProductionLineId = lines[0].Id, ToolStatus = "Running",
                CumulativeRuntimeHours = 1840.5, HoursSinceLastPM = 142.5,
                NextScheduledPM = DateTime.UtcNow.AddDays(7)
            },
            new ToolMaintenanceLog
            {
                ProductionLineId = lines[1].Id, ToolStatus = "PM_Due",
                CumulativeRuntimeHours = 3200.0, HoursSinceLastPM = 498.0,
                NextScheduledPM = DateTime.UtcNow.AddHours(12)
            },
            new ToolMaintenanceLog
            {
                ProductionLineId = lines[2].Id, ToolStatus = "Running",
                CumulativeRuntimeHours = 950.0, HoursSinceLastPM = 55.0,
                NextScheduledPM = DateTime.UtcNow.AddDays(14)
            }
        );

        // ─── Seed recipe versions ─────────────────────────────────────────
        await context.RecipeVersions.AddRangeAsync(
            new RecipeVersion { RecipeName = "DRAM-16Gb-v3", VersionNumber = 1, ParameterPayloadXml = "<params><temp>850</temp><time>30</time><pressure>2.0</pressure></params>", Author = "jane.engineer", CreatedAt = DateTime.UtcNow.AddDays(-30) },
            new RecipeVersion { RecipeName = "DRAM-16Gb-v3", VersionNumber = 2, ParameterPayloadXml = "<params><temp>855</temp><time>28</time><pressure>2.1</pressure></params>", Author = "jane.engineer", CreatedAt = DateTime.UtcNow.AddDays(-15) },
            new RecipeVersion { RecipeName = "DRAM-16Gb-v3", VersionNumber = 3, ParameterPayloadXml = "<params><temp>860</temp><time>25</time><pressure>2.0</pressure></params>", Author = "admin", CreatedAt = DateTime.UtcNow.AddDays(-3) },
            new RecipeVersion { RecipeName = "NAND-256Gb-v2", VersionNumber = 1, ParameterPayloadXml = "<params><temp>900</temp><time>45</time><pressure>1.5</pressure></params>", Author = "jane.engineer", CreatedAt = DateTime.UtcNow.AddDays(-20) },
            new RecipeVersion { RecipeName = "NAND-256Gb-v2", VersionNumber = 2, ParameterPayloadXml = "<params><temp>905</temp><time>42</time><pressure>1.6</pressure></params>", Author = "jane.engineer", CreatedAt = DateTime.UtcNow.AddDays(-5) },
            new RecipeVersion { RecipeName = "DRAM-8Gb-v5", VersionNumber = 5, ParameterPayloadXml = "<params><temp>820</temp><time>20</time><pressure>2.5</pressure></params>", Author = "admin", CreatedAt = DateTime.UtcNow.AddDays(-10) }
        );

        // ─── Seed ECO records ─────────────────────────────────────────────
        await context.EcoRecords.AddRangeAsync(
            new EcoRecord { Title = "Increase oxidation temp for DRAM-16Gb", RecipeName = "DRAM-16Gb-v3", ChangeDetail = "Oxidation temperature increased from 850°C to 860°C to improve gate quality", ApprovedBy = "admin", Status = "Approved", CreatedAt = DateTime.UtcNow.AddDays(-3) },
            new EcoRecord { Title = "Reduce etch time for NAND-256Gb", RecipeName = "NAND-256Gb-v2", ChangeDetail = "Etch time reduced from 45s to 42s to reduce over-etch", ApprovedBy = "jane.engineer", Status = "Pending", CreatedAt = DateTime.UtcNow.AddDays(-1) }
        );

        // ─── Seed CAPA actions ────────────────────────────────────────────
        await context.CapaActions.AddRangeAsync(
            new CapaAction { LotId = lot3.Id, ActionDescription = "Investigate particle contamination source in PHOTOLITHO_3 bay", AssignedTo = "jane.engineer", Status = "Open", ApprovedBy = "admin", CreatedAt = DateTime.UtcNow.AddHours(-20) },
            new CapaAction { LotId = null, ActionDescription = "Review and update CMP pad conditioning procedure", AssignedTo = "john.operator", Status = "In Progress", ApprovedBy = "admin", CreatedAt = DateTime.UtcNow.AddDays(-2) }
        );

        // ─── Seed shift logs ──────────────────────────────────────────────
        await context.ShiftLogs.AddRangeAsync(
            new ShiftLog { ShiftName = "Night Shift", SummaryNotes = "Line Alpha running BATCH-001 at PHOTOLITHO_1. Line Beta completed BATCH-002. No critical issues.", ActiveAlertsSummary = "2 open alerts on Lines Alpha and Beta", HandoverCompleted = true, CompletedBy = "john.operator", CreatedAt = DateTime.UtcNow.AddHours(-8) },
            new ShiftLog { ShiftName = "Day Shift", SummaryNotes = "Line Gamma started BATCH-006. LOT-2026-003 placed on hold due to contamination concern.", ActiveAlertsSummary = "1 open alert on Line Gamma (acknowledged)", HandoverCompleted = true, CompletedBy = "jane.engineer", CreatedAt = DateTime.UtcNow.AddHours(-1) }
        );

        // ─── Seed suppliers and materials ────────────────────────────────
        var supplier1 = new Supplier { Name = "Silicon Wafers Inc.", ContactEmail = "orders@siliconwafers.com", ContactPhone = "555-0100", LeadTimeDays = 14, Rating = "A", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-90) };
        var supplier2 = new Supplier { Name = "Specialty Gases Co.", ContactEmail = "sales@specialtygases.com", ContactPhone = "555-0200", LeadTimeDays = 7, Rating = "A", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-90) };
        var supplier3 = new Supplier { Name = "Photoresist Supply LLC", ContactEmail = "info@photoresist.com", ContactPhone = "555-0300", LeadTimeDays = 21, Rating = "B", IsActive = true, CreatedAt = DateTime.UtcNow.AddDays(-60) };
        await context.Suppliers.AddRangeAsync(supplier1, supplier2, supplier3);
        await context.SaveChangesAsync();

        var materials = new List<Material>
        {
            new() { PartNumber = "SI-WAFER-300", Name = "300mm Silicon Wafer", Category = MaterialCategory.RawWafer, Unit = "ea", QuantityOnHand = 450, ReorderPoint = 200, ReorderQuantity = 500, UnitCost = 85.00m, SupplierId = supplier1.Id, LastReceivedAt = DateTime.UtcNow.AddDays(-10), CreatedAt = DateTime.UtcNow.AddDays(-90) },
            new() { PartNumber = "CVD-N2-99.99", Name = "Nitrogen Gas (99.99%)", Category = MaterialCategory.Gas, Unit = "L", QuantityOnHand = 8500, ReorderPoint = 5000, ReorderQuantity = 10000, UnitCost = 0.05m, SupplierId = supplier2.Id, LastReceivedAt = DateTime.UtcNow.AddDays(-5), CreatedAt = DateTime.UtcNow.AddDays(-90) },
            new() { PartNumber = "CVD-SIH4", Name = "Silane Gas", Category = MaterialCategory.Gas, Unit = "L", QuantityOnHand = 1200, ReorderPoint = 800, ReorderQuantity = 2000, UnitCost = 1.20m, SupplierId = supplier2.Id, LastReceivedAt = DateTime.UtcNow.AddDays(-7), CreatedAt = DateTime.UtcNow.AddDays(-90) },
            new() { PartNumber = "PHOTORESIST-DUV", Name = "DUV Photoresist", Category = MaterialCategory.Chemical, Unit = "mL", QuantityOnHand = 350, ReorderPoint = 200, ReorderQuantity = 500, UnitCost = 45.00m, SupplierId = supplier3.Id, LastReceivedAt = DateTime.UtcNow.AddDays(-15), CreatedAt = DateTime.UtcNow.AddDays(-60) },
            new() { PartNumber = "CMP-SLURRY-1", Name = "CMP Slurry (Oxide)", Category = MaterialCategory.Chemical, Unit = "L", QuantityOnHand = 80, ReorderPoint = 100, ReorderQuantity = 200, UnitCost = 120.00m, SupplierId = supplier3.Id, LastReceivedAt = DateTime.UtcNow.AddDays(-20), CreatedAt = DateTime.UtcNow.AddDays(-60) },
            new() { PartNumber = "ETCH-CF4", Name = "CF4 Etch Gas", Category = MaterialCategory.Gas, Unit = "L", QuantityOnHand = 3200, ReorderPoint = 2000, ReorderQuantity = 5000, UnitCost = 0.80m, SupplierId = supplier2.Id, LastReceivedAt = DateTime.UtcNow.AddDays(-3), CreatedAt = DateTime.UtcNow.AddDays(-90) },
            new() { PartNumber = "IMPLANT-B11", Name = "Boron-11 Implant Source", Category = MaterialCategory.Gas, Unit = "mg", QuantityOnHand = 45, ReorderPoint = 20, ReorderQuantity = 50, UnitCost = 350.00m, SupplierId = supplier2.Id, LastReceivedAt = DateTime.UtcNow.AddDays(-12), CreatedAt = DateTime.UtcNow.AddDays(-45) }
        };
        await context.Materials.AddRangeAsync(materials);
        await context.SaveChangesAsync();

        // ─── Seed purchase orders ─────────────────────────────────────────
        var po1 = new PurchaseOrder
        {
            PONumber = "PO-2026-001", SupplierId = supplier1.Id, Status = "Received",
            TotalAmount = 42500.00m, CreatedBy = "admin", ApprovedBy = "admin",
            OrderedAt = DateTime.UtcNow.AddDays(-20), ExpectedAt = DateTime.UtcNow.AddDays(-6),
            ReceivedAt = DateTime.UtcNow.AddDays(-5)
        };
        var po2 = new PurchaseOrder
        {
            PONumber = "PO-2026-002", SupplierId = supplier3.Id, Status = "Ordered",
            TotalAmount = 24000.00m, CreatedBy = "jane.engineer", ApprovedBy = "admin",
            OrderedAt = DateTime.UtcNow.AddDays(-2), ExpectedAt = DateTime.UtcNow.AddDays(19)
        };
        var po3 = new PurchaseOrder
        {
            PONumber = "PO-2026-003", SupplierId = supplier2.Id, Status = "Approved",
            TotalAmount = 16000.00m, CreatedBy = "jane.engineer", ApprovedBy = "admin",
            OrderedAt = DateTime.UtcNow.AddHours(-12), ExpectedAt = DateTime.UtcNow.AddDays(7)
        };
        await context.PurchaseOrders.AddRangeAsync(po1, po2, po3);
        await context.SaveChangesAsync();

        // PO lines
        await context.PurchaseOrderLines.AddRangeAsync(
            new PurchaseOrderLine { PurchaseOrderId = po1.Id, MaterialId = materials[0].Id, QuantityOrdered = 500, QuantityReceived = 500, UnitCost = 85.00m },
            new PurchaseOrderLine { PurchaseOrderId = po2.Id, MaterialId = materials[3].Id, QuantityOrdered = 500, QuantityReceived = 0, UnitCost = 45.00m },
            new PurchaseOrderLine { PurchaseOrderId = po2.Id, MaterialId = materials[4].Id, QuantityOrdered = 100, QuantityReceived = 0, UnitCost = 120.00m },
            new PurchaseOrderLine { PurchaseOrderId = po3.Id, MaterialId = materials[1].Id, QuantityOrdered = 10000, QuantityReceived = 0, UnitCost = 0.05m },
            new PurchaseOrderLine { PurchaseOrderId = po3.Id, MaterialId = materials[5].Id, QuantityOrdered = 5000, QuantityReceived = 0, UnitCost = 0.80m }
        );

        // ─── Seed material reservations ──────────────────────────────────
        await context.MaterialReservations.AddRangeAsync(
            new MaterialReservation { MaterialId = materials[0].Id, LotId = lot1.Id, QuantityReserved = 25, Status = "Active", RouteStepName = "WAFER_PREP", ReservedAt = DateTime.UtcNow.AddHours(-6) },
            new MaterialReservation { MaterialId = materials[2].Id, LotId = lot1.Id, QuantityReserved = 50, Status = "Active", RouteStepName = "CVD_DEPOSITION", ReservedAt = DateTime.UtcNow.AddHours(-4) },
            new MaterialReservation { MaterialId = materials[0].Id, LotId = lot2.Id, QuantityReserved = 25, Status = "Active", RouteStepName = "WAFER_PREP", ReservedAt = DateTime.UtcNow.AddHours(-12) },
            new MaterialReservation { MaterialId = materials[3].Id, LotId = lot3.Id, QuantityReserved = 30, Status = "Released", RouteStepName = "PHOTOLITHO_3", ReservedAt = DateTime.UtcNow.AddHours(-22), ReleasedAt = DateTime.UtcNow.AddHours(-20) }
        );

        await context.SaveChangesAsync();
    }
}