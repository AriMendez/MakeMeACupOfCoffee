using System.Runtime.CompilerServices;

MakeCoffee();

Console.WriteLine("");
Console.WriteLine("********");
Console.WriteLine("");

await MakeCoffeeAsync();

#region SyncMethods
void MakeCoffee()
{
    var starttime = DateTime.Now;
    Console.WriteLine("Making coffee... NO ASYNC");
    var shotespresso = Espresso();
    var leche = LecheEspumada();

    VertirEnVaso(shotespresso);
    VertirEnVaso(leche);
    
    Console.WriteLine("- Agregar azúcar");
    Console.WriteLine("- Servir en la mesa");
    Console.WriteLine("- Beber");

    Console.WriteLine("Tiempo de preparación: " + (DateTime.Now - starttime).TotalSeconds + " segundos");
}

string LecheEspumada()
{
    Console.WriteLine("- Calentado la leche");
    Console.WriteLine("- Espumando la leche");
    Thread.Sleep(5000);
    Console.WriteLine("- Leche completa");

    return "Leche espumada";
}

void Molercafe()
{
    Console.WriteLine("--> Comienza molienda");
    Thread.Sleep(5000);
    Console.WriteLine("--> Molienda completa");
}

void Prepararshot()
{
    Console.WriteLine("--> Pesar cafe");
    Console.WriteLine("--> Comprimir cafe");
    Console.WriteLine("--> Colocar portafiltro en la maquina");
    Thread.Sleep(3000);
}

string Espresso()
{
    Console.WriteLine("- Moler cafe");
    Molercafe();
    Console.WriteLine("- Preparar shot");
    Prepararshot();
    Console.WriteLine("- Extrayendo espresso");
    Thread.Sleep(5000);
    Console.WriteLine("- Espresso listo");

    return "Espresso";
}
        
void VertirEnVaso(string contenido)
{
    Console.WriteLine("--- Vertiendo " + contenido + " en el vaso...");
}

#endregion

#region AsyncMethods

async Task MakeCoffeeAsync()
{
    var starttime = DateTime.Now;
    Console.WriteLine("Making coffee... ASYNC");
    var shotespressoasync =  EspressoAsync();
    var lecheasync = LecheEspumadaAsync();

    Console.WriteLine("- Agregar azúcar");
    
    VertirEnVaso(await shotespressoasync);
    VertirEnVaso(await lecheasync);

    Console.WriteLine("- Servir en la mesa");
    Console.WriteLine("- Beber");

    Console.WriteLine("Tiempo de preparación: " + (DateTime.Now - starttime).TotalSeconds + " segundos");
}

async Task<string> EspressoAsync()
{
    Console.WriteLine("- Moler cafe");
    await MolercafeAsync();
    Console.WriteLine("- Preparar shot");
    await PrepararshotAsync();
    Console.WriteLine("- Extrayendo espresso");
    await 5;
    Console.WriteLine("- Espresso listo");

    return "Espresso";
}

async Task MolercafeAsync()
{
    Console.WriteLine("--> Comienza molienda");
    await Task.Delay(5000);
    Console.WriteLine("--> Molienda completa");
}

async Task PrepararshotAsync()
{
    Console.WriteLine("--> Pesar cafe");
    Console.WriteLine("--> Comprimir cafe");
    Console.WriteLine("--> Colocar portafiltro en la maquina");
    await Task.Delay(3000);
}

async Task<string> LecheEspumadaAsync()
{
    Console.WriteLine("- Calentado la leche");
    Console.WriteLine("- Espumando la leche");
    await Task.Delay(5000);
    Console.WriteLine("- Leche completa");

    return "Leche espumada";
}

#endregion

#region ExtensionMethods
public static class ExtensionMethods
{      
    public static TaskAwaiter GetAwaiter(this int seconds)
    {
        return Task.Delay(TimeSpan.FromSeconds(seconds)).GetAwaiter();
    }
    public static TaskAwaiter GetAwaiter(this string message)
    {
        Console.WriteLine(message);
        return Task.Delay(TimeSpan.FromSeconds(5)).GetAwaiter();
    }
}
#endregion