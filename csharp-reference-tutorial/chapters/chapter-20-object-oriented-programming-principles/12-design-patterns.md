# Design Patterns

Few years after the onset of the object -oriented paradigm it was found that 
there are many situations, which  occur frequently during software 
development, such as a class, which must have only one instance within the 
entire application. 
Design patterns appeared as proven and highly -efficient solutions to the 
most common problems of object -oriented modeling . Design patterns 
are systematically described in the eponymous book by Eri ch Gamma & Co. 


"Design Patterns: Elements of Reusable Object -Oriented Software " 
(ISBN 0 -201-63361-2). The patterns in this book are called “the GoF 
patterns” or “classical design patterns”. 
This is one of the few books in the field of computer science, 
which remain current 15 years after publishing. Design 
patterns complement the basic principles of OOP with well-
known solutions of well -known problems. A good place to 
start studyin g the design patterns is the ir Wikipedia article: 
en.wikipedia.org/wiki/Design_pattern (computer science) . You 
may also check the "Data & Object Factory" patterns catalog 
http://www.dofactory.com/Patterns/Patterns.aspx, where the authors  provide 
C# implementation of the classical GoF patterns.

## The Singleton Design Pattern

This is the most popular and most freq uently used design pattern. It allows a 
class to have only one instance and defines where it has to be taken from. 
Typical examples are classes, which  define references to singular entities (a 
virtual machine, operating system, window manager in a graphica l application 
or a file system) as well as classes of the next pattern (factory).

### The Singleton Design Pattern – Example

Here is a sample implementation of the singleton design pattern: 
Singleton.cs 
```csharp
public class Singleton 
{ 
 // The single instance 
 private static Singleton instance; 
 
 // Initialize the single instance 
 static Singleton() 
 { 
  instance = new Singleton(); 
 } 
 
 // The property for retrieving the single instance 
 public static Singleton Instance 
 { 
  get { return instance; } 
 } 
 
 // Private constructor: protects against direct instantiation 
 private Singleton() { } 
} 


```
We have a hidden (private) constructor in order to limit external 
instantiations. We have a static variable, which holds the only instance. We 
initialize it only once in the static constructor of the class. The property for 
retrieving the single instance is usually called Instance. 
The pattern can undergo many optimizations, such as the so called " lazy 
initialization" of the only variable, i n order to save memory, but this is its 
classical form.

## The Factory Method Design Pattern

Factory method is another very common design pattern. It is intended for 
"producing" objects . The instantiation of an object is not performed 
directly, but rather by the factory method. This allows the factory method to 
decide which specific instance to create  from a family of classes implementing 
a common interface . The solution can depend on the environment, a 
parameter or some system setting.

### The Factory Method Design Pattern – Example

Factory methods encapsulate object creation . This is useful if the creation 
process is very complicated – if it depends on settings in configuration files or 
input data by the user. 
Suppose we have a class which contains graphics files (png, jpeg, bmp, etc.) 
and creates reduced size copies of them (the so called thumbnails). A 
variety of formats are supported, each represented by a class: 
```csharp
public class Thumbnail 
{ 
 // … 
} 
 
public interface Image 
{ 
 Thumbnail CreateThumbnail(); 
} 
 
public class GifImage : Image 
{ 
 public Thumbnail CreateThumbnail() 
 { 
  // … Create a GIF thumbnail here … 
  return gifThumbnail; 
 } 
} 
 
public class JpegImage : Image 

{ 
 public Thumbnail CreateThumbnail() 
 { 
  // … Create a JPEG thumbnail here … 
  return jpegThumbnail; 
 } 
} 
Here is how the class holding an album of images looks like: 
public class ImageCollection 
{ 
 private IList<Image> images; 
 
 public ImageCollection(IList<Image> images) 
 { 
  this.images = images; 
 } 
 
 public IList<Thumbnail> CreateThumbnails() 
 { 
  IList<Thumbnail> thumbnails = 
   new List<Thumbnail>(images.Count); 
  foreach (Image thumb in images) 
  { 
   thumbnails.Add(thumb.CreateThumbnail()); 
  } 
  return thumbnails; 
 } 
} 
```
The client of the program may require thumbnails of all images in the album: 
```csharp
public class Example 
{ 
 static void Main() 
 { 
  IList<Image> images = new List<Image>(); 
 
  images.Add(new JpegImage()); 
  images.Add(new GifImage()); 
 
  ImageCollection imageRepository = 
   new ImageCollection(images); 
 

  Console.WriteLine(imageRepository.CreateThumbnails()); 
 } 
}
```

## Other Design Patterns

There are dozens of other well -known design patterns, but we are not going 
to discuss them. The more inquisitive readers can look up "Desig n Patterns" 
on the internet and learn what other design patterns, such as Abstract 
Factory, Prototype, Adapter, Composite, Façade, Command, Observer, 
Iterator, etc. serve for and how they  are put into use. If you pursue .NET 
development more seriously, you  will see for yourselves that the whole 
standard library (FCL) is built on the principles of OOP and the classic design 
patterns are very actively used.