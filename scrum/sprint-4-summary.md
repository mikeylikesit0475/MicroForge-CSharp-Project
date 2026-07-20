# Sprint 4 Summary - "Knowledge at Your Fingertips"

> **Dates:** August 25 – September 7, 2026  
> **Status:** ✅ Complete  
> **Build:** 0 errors, 0 warnings, 14 tests passing

---

## Sprint Goal

> Implement the RAG document intelligence pipeline (PDF upload → text extraction → chunking → embedding → vector store → semantic search) and the agent chat UI, so that the AI agent can ground its responses in uploaded manufacturing documents and users have a conversational interface.

---

## Completed Stories

| Story ID | Story Title | Points | Status |
|----------|-------------|--------|--------|
| MF-019 | Document Upload API | 3 | ✅ Done |
| MF-020 | PDF Text Extraction & Chunking | 5 | ✅ Done |
| MF-021 | Embedding Generation & Vector Store | 8 | ✅ Done |
| MF-022 | Semantic Search API | 5 | ✅ Done |
| MF-023 | Document Management UI | 5 | ✅ Done |
| MF-018 | Agent Chat UI Component | 5 | ✅ Done |

**Total Points:** 31 / 31 (100% complete)

---

## Key Deliverables

### 1. Document Upload API with Validation ✅

**Endpoint:** `POST /api/documents/upload`

- Accepts PDF, TXT, and MD files
- Enforces 10MB file size limit
- Validates content type
- Returns `DocumentUploadResponse` with document ID

**Implementation:**
- `src/MicroForge.Infrastructure/RAG/DocumentService.cs` - Upload orchestration with validation
- `src/MicroForge.Web/Controllers/DocumentController.cs` - API endpoint with role-based authorization

### 2. PDF Text Extraction ✅

**Implementation:**
- `src/MicroForge.Infrastructure/RAG/PdfTextExtractor.cs` - New service using UglyToad.PdfPig
- Extracts text page-by-page, preserving line structure
- Handles multi-page PDFs with proper word/line reconstruction

**Package Added:**
- `UglyToad.PdfPig` v1.7.0-custom-5 (prerelease)

### 3. Document Chunking with Overlap ✅

**Implementation:**
- `src/MicroForge.Infrastructure/RAG/DocumentChunker.cs`
- 500-token chunks with 50-token overlap
- Respects paragraph/word boundaries
- Metadata includes source file and chunk index

### 4. Embedding Generation & Vector Store ✅

**Implementation:**
- `src/MicroForge.Infrastructure/RAG/EmbeddingService.cs`
  - Supports mock embeddings (deterministic hash-based) for demo mode
  - Configurable Ollama endpoint for real embeddings
- `src/MicroForge.Infrastructure/RAG/VectorStore.cs`
  - In-memory HNSW-style vector index
  - Cosine similarity search
  - Delete by document ID support

### 5. Semantic Search API ✅

**Endpoint:** `POST /api/documents/search`

**Request:**
```json
{
  "query": "temperature range photolithography",
  "topK": 5,
  "category": "SOP"
}
```

**Response:**
```json
{
  "results": [
    {
      "documentId": 3,
      "documentTitle": "Photolithography SOP v2.1",
      "chunkContent": "The recommended temperature range...",
      "similarity": 0.89,
      "chunkIndex": 12
    }
  ],
  "queryEmbeddingTimeMs": 45,
  "searchTimeMs": 12
}
```

**Implementation:**
- `src/MicroForge.Infrastructure/RAG/SemanticSearchService.cs`

### 6. Document Management UI ✅

**Page:** `/documents`

**Features:**
- Document list with status (Processed/Pending)
- File upload modal with drag-and-drop
- Category filtering (SOP, Specification, Report, Manual, Other)
- Real-time search with semantic results
- Document deletion with confirmation
- File type icons (PDF, TXT, MD)
- File size formatting

**Implementation:**
- `src/MicroForge.Web/Components/Pages/Documents.razor`
- Enhanced CSS in `src/MicroForge.Web/wwwroot/css/app.css`

### 7. Agent Chat UI Component ✅

**Page:** `/agent`

**Features:**
- Conversational chat interface
- User/assistant message distinction
- Tool invocation badges showing which tools were used
- Auto-scrolling message list
- Token-based authentication

**Implementation:**
- `src/MicroForge.Web/Components/Pages/Agent.razor`
- Tool invocation display with badges

---

## Architecture Verification

### RAG Pipeline Flow

```
Upload → Validate → Extract Text → Chunk → Embed → Vector Store → Search
  │         │           │           │        │         │            │
  │         │           │           │        │         │            └─► Cosine similarity
  │         │           │           │        │         └─► In-memory HNSW index
  │         │           │           │        └─► 384-dim mock/embedding
  │         │           │           └─► 500 tokens, 50 overlap
  │         │           └─► PdfPig (PDF) or StreamReader (TXT/MD)
  │         └─► 10MB limit, allowed types
  └─► Form multipart upload
```

### Dependency Injection Registration

```csharp
// RAG Pipeline (src/MicroForge.Infrastructure/DependencyInjection.cs)
services.Configure<ChunkerOptions>(configuration.GetSection("RAG"));
services.Configure<EmbeddingOptions>(configuration.GetSection("AI"));
services.AddSingleton<IDocumentChunker, DocumentChunker>();
services.AddSingleton<IEmbeddingService, EmbeddingService>();
services.AddSingleton<IPdfTextExtractor, PdfTextExtractor>();
services.AddSingleton<RAGVectorStore, RAGInMemoryVectorStore>();
services.AddScoped<ISemanticSearchService, SemanticSearchService>();
services.AddScoped<IDocumentService, DocumentService>();
```

---

## Definition of Done Verification

| Acceptance Criteria | Status |
|---------------------|--------|
| PDF upload accepts valid files, rejects non-PDF and oversized files | ✅ |
| Text extraction produces readable plain text from multi-page PDFs | ✅ |
| Chunking produces overlapping 500-token chunks with metadata | ✅ |
| Embeddings are generated and stored in a searchable vector index | ✅ |
| Semantic search returns top-K results ranked by similarity | ✅ |
| Document management UI allows upload, status viewing, and deletion | ✅ |
| Agent chat UI sends messages and displays tool-grounded responses | ✅ |
| Semantic search integrates with agent framework as a grounding source | ✅ |

---

## Technical Notes

### PDF Text Extraction
- Uses UglyToad.PdfPig library (prerelease v1.7.0-custom-5)
- Words are grouped by Y-position to reconstruct lines
- Pages are separated by blank lines in output

### Mock Embeddings
- Default mode uses deterministic hash-based embeddings
- 384-dimensional vectors (compatible with MiniLM)
- Enables demo/testing without external LLM dependency

### Vector Store
- In-memory storage (lost on restart)
- Suitable for demo/prototype scale
- Production would require persistent vector DB (Qdrant, Pinecone, etc.)

---

## Build & Test Status

```
Build succeeded.
    0 Warning(s)
    0 Error(s)

Test Results:
- Unit Tests: 12 passing
- Integration Tests: 1 passing
- Agent Tests: 1 passing
- Total: 14 tests passing
```

---

## Files Modified/Created

### New Files
- `src/MicroForge.Infrastructure/RAG/PdfTextExtractor.cs`
- `src/MicroForge.Web/wwwroot/css/app.css` (enhanced)

### Modified Files
- `src/MicroForge.Infrastructure/RAG/DocumentService.cs` - Added PDF support, validation
- `src/MicroForge.Infrastructure/DependencyInjection.cs` - Registered PdfTextExtractor
- `src/MicroForge.Web/Components/Pages/Documents.razor` - Full UI rewrite
- `src/MicroForge.Web/Components/Pages/Agent.razor` - Tool invocation display

### Project File Changes
- `src/MicroForge.Infrastructure/MicroForge.Infrastructure.csproj` - Added UglyToad.PdfPig package

---

## Next Steps (Sprint 5)

Sprint 5 "The Pulse of Production" will implement:
- Simulated sensor data generator
- Event ingestion pipeline with Channels
- Anomaly detection service
- Alert notification service
- Events history API & UI

---

## Retrospective Notes

### What Went Well
- RAG infrastructure was already in place from previous work
- PDF extraction library integration was straightforward
- UI components built on existing patterns

### Challenges
- PdfPig API differences (GetText vs GetWords)
- Blazor InputFile component requires EditForm wrapper

### Improvements for Future Sprints
- Consider persistent vector store for production
- Add document OCR for scanned PDFs
- Implement document versioning
