using System.Reflection;

FindLinkedDlls();

void FindLinkedDlls()
{
  var assembly = Assembly.GetEntryAssembly()!;
  var path = assembly.Location;
  var directory = Path.GetDirectoryName(path)!;
  var dlls = Directory.GetFiles(directory, "*.dll").Except([path]);
  foreach (var dll in dlls)
  {
    LoadDllsAndFunctions(dll);
  }
}

void LoadDllsAndFunctions(string dll)
{
  Console.ForegroundColor = ConsoleColor.Green;
  Console.WriteLine(dll);
  Console.ForegroundColor = ConsoleColor.White;

  var assembly = Assembly.LoadFrom(dll);

  foreach (var type in assembly.GetTypes())
  {
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"  → {type.FullName}");
    Console.ForegroundColor = ConsoleColor.White;

    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
    {
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine($"    ⋅ {method.Name}");
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
