using System;

public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }

    public Persona(string nombre, int edad)
    {
        Nombre = nombre;
        Edad = edad;
    }
}

public class Cuenta
{
    protected Persona Titular;
    protected double Cantidad;

    public Cuenta(Persona titular, double cantidad)
    {
        Titular = titular;
        Cantidad = cantidad;
    }

    public Persona GetTitular() => Titular;
    public double GetCantidad() => Cantidad;

    public virtual void Depositar(double monto)
    {
        if (monto > 0) Cantidad += monto;
    }

    public virtual void Retirar(double monto)
    {
        if (monto > 0) Cantidad -= monto;
    }

    public virtual string Mostrar()
    {
        return $"Titular: {Titular.Nombre}, Cantidad: {Cantidad}";
    }
}

public class CuentaJoven : Cuenta
{
    // Bonificación en tanto por ciento
    public double Bonificacion { get; set; }

    // Constructor
    public CuentaJoven(Persona titular, double cantidad, double bonificacion)
        : base(titular, cantidad)
    {
        Bonificacion = bonificacion;
    }

    // Setters y getters (además de la propiedad)
    public void SetBonificacion(double bonificacion) => Bonificacion = bonificacion;
    public double GetBonificacion() => Bonificacion;

    // Titular válido: mayor de edad y menor de 25
    public bool EsTitularValido()
    {
        int edad = GetTitular().Edad;
        return edad >= 18 && edad < 25;
    }

    // Retirada sólo si titular válido
    public override void Retirar(double monto)
    {
        if (EsTitularValido())
        {
            base.Retirar(monto);
        }
    }

    // Mostrar información específica
    public override string Mostrar()
    {
        return $"Cuenta Joven - Bonificación: {Bonificacion}%";
    }
}

class Program
{
    static void Main()
    {
        var titular = new Persona("Ana", 20);
        var cuenta = new CuentaJoven(titular, 1000, 10);
        Console.WriteLine(cuenta.Mostrar());
        cuenta.Retirar(100);
        Console.WriteLine($"Cantidad tras retiro: {cuenta.GetCantidad()}");
    }
}

