using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string tipoCuenta = GetValidTipoCuenta();
        string nombre = GetValidSalidaAlfabetica("Ingrese el nombre de la persona:");
        string DPI = GetValidDpi();
        string Direccion = GetValidDireccion();
        string Telefono = GetValidTelefono();

        decimal Balance = 2500.00m;

        Account cuenta = new Account(tipoCuenta, nombre, DPI, Direccion, Telefono, Balance);

        while (true)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Ver información de la cuenta");
            Console.WriteLine("2. Comprar producto financiero");
            Console.WriteLine("3. Vender producto financiero");
            Console.WriteLine("4. Abonar a cuenta");
            Console.WriteLine("5. Simular paso del tiempo");
            Console.WriteLine("6. Mantenimiento de cuentas de terceros");
            Console.WriteLine("7. Realizar transferencias a otras cuentas");
            Console.WriteLine("8. Pago de servicios");
            Console.WriteLine("9. Imprimir informe de transacciones");
            Console.WriteLine("10. Salir");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    cuenta.MostrarInfo();
                    break;
                case "2":
                    cuenta.ComprarProductoFinanciero();
                    break;
                case "3":
                    cuenta.VenderProductoFinanciero();
                    break;
                case "4":
                    cuenta.Depositar_a_Cuenta();
                    break;
                case "5":
                    cuenta.SimularPasodelTiempo();
                    break;
                case "6":
                    cuenta.Mantenimiento_Cuenta_de_Terceros();
                    break;
                case "7":
                    cuenta.RealizarTransferencia();
                    break;
                case "8":
                    cuenta.PagarServicios();
                    break;
                case "9":
                    cuenta.ImprimirReporteTransaccion();
                    break;
                case "10":
                    return;
                default:
                    Console.WriteLine("Opción inválida. Por favor, intente de nuevo.");
                    break;
            }
        }
    }

    static string GetValidTipoCuenta()
    {
        while (true)
        {
            Console.WriteLine("Ingrese el tipo de cuenta (1. Monetaria Quetzales, 2. Monetaria Dólares, 3. Ahorro Quetzales, 4. Ahorro Dólares):");
            string input = Console.ReadLine();
            if (input == "1" || input == "2" || input == "3" || input == "4")
            {
                return input switch
                {
                    "1" => "Monetaria Quetzales",
                    "2" => "Monetaria Dólares",
                    "3" => "Ahorro Quetzales",
                    "4" => "Ahorro Dólares",
                    _ => throw new InvalidOperationException("Opción inválida.")
                };
            }
            else
            {
                Console.WriteLine("Opción inválida. Por favor, intente de nuevo.");
            }
        }
    }

    static string GetValidDireccion()
    {
        while (true)
        {
            Console.WriteLine("Ingrese la dirección:");
            string input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z0-9\s]+$"))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese solo caracteres alfanuméricos.");
            }
        }
    }

    static string GetValidSalidaAlfabetica(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese solo caracteres alfabéticos.");
            }
        }
    }

    static string GetValidDpi()
    {
        while (true)
        {
            Console.WriteLine("Ingrese el DPI (5 caracteres numéricos):");
            string input = Console.ReadLine();
            if (input.Length == 5 && Regex.IsMatch(input, @"^\d{5}$"))
            {
                return input;
            }
            else
            {
                Console.WriteLine("DPI inválido. Debe contener exactamente 5 caracteres numéricos.");
            }
        }
    }

    static string GetValidTelefono()
    {
        while (true)
        {
            Console.WriteLine("Ingrese el número de teléfono (8 dígitos):");
            string input = Console.ReadLine();
            if (input.Length == 8 && Regex.IsMatch(input, @"^\d{8}$"))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Teléfono inválido. Debe contener exactamente 8 caracteres numéricos.");
            }
        }
    }
}

class Account
{
    public string TipoCuenta { get; set; }
    public string Nombre { get; set; }
    public string DPI { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public decimal BALANCE { get; set; }
    private int depositCount = 0;
    private List<Transaction> transactions = new List<Transaction>();
    private List<ThirdPartyAccount> thirdPartyAccounts = new List<ThirdPartyAccount>();
    private int thirdPartyAccountIdCounter = 1;

    public Account(string tipoCuenta, string nombre, string Dpi, string direccion, string telefono, decimal Balance)
    {
        TipoCuenta = tipoCuenta;
        Nombre = nombre;
        DPI = Dpi;
        Direccion = direccion;
        Telefono = telefono;
        BALANCE = Balance;
    }

    public void MostrarInfo()
    {
        Console.WriteLine($"Tipo de Cuenta: {TipoCuenta}");
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"DPI: {DPI}");
        Console.WriteLine($"Dirección: {Direccion}");
        Console.WriteLine($"Teléfono: {Telefono}");
        Console.WriteLine($"Saldo: Q{BALANCE}");
        Console.WriteLine("Transacciones:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
    }

    public void ComprarProductoFinanciero()
    {
        decimal cantidad = BALANCE * 0.10m;
        BALANCE -= cantidad;
        Console.WriteLine($"Se ha comprado un producto financiero. Saldo actual: Q{BALANCE}");
        transactions.Add(new Transaction(DateTime.Now, cantidad, "Débito", "Compra de producto financiero"));
    }

    public void VenderProductoFinanciero()
    {
        if (BALANCE > 500.00m)
        {
            decimal cantidad = BALANCE * 0.11m;
            BALANCE += cantidad;
            Console.WriteLine($"Se ha vendido un producto financiero. Saldo actual: Q{BALANCE}");
            transactions.Add(new Transaction(DateTime.Now, cantidad, "Crédito", "Venta de producto financiero"));
        }
        else
        {
            Console.WriteLine("No se recomienda realizar la transacción debido al saldo actual.");
        }
    }

    public void Depositar_a_Cuenta()
    {
        if (depositCount < 2 && BALANCE > 500.00m)
        {
            BALANCE *= 2;
            depositCount++;
            Console.WriteLine($"Se ha duplicado el saldo de la cuenta. Saldo actual: Q{BALANCE}");
            transactions.Add(new Transaction(DateTime.Now, BALANCE, "Crédito", "Depósito a cuenta"));
        }
        else
        {
            Console.WriteLine("No se puede realizar el depósito. Límite alcanzado o saldo insuficiente.");
        }
    }

    public void SimularPasodelTiempo()
    {
        Console.WriteLine("Ingrese el período de capitalización (1. Una vez al mes, 2. Dos veces al mes):");
        int Periodo = int.Parse(Console.ReadLine());
        int DiasenMeses = 30;
        int Capitalizaciondeperiodo = Periodo == 1 ? DiasenMeses : DiasenMeses / 2; // Cambio aquí para considerar la capitalización según el período elegido

        decimal interes = BALANCE * 0.02m * Capitalizaciondeperiodo / 360; // Fórmula de interés simple
        BALANCE += interes;
        Console.WriteLine($"Se ha simulado el paso del tiempo. Saldo actual: Q{BALANCE}");
        transactions.Add(new Transaction(DateTime.Now, interes, "Crédito", "Interés ganado"));

    }

    public void Mantenimiento_Cuenta_de_Terceros()
    {
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1. Crear cuenta de tercero");
        Console.WriteLine("2. Eliminar cuenta de tercero");
        Console.WriteLine("3. Actualizar cuenta de tercero");
        Console.WriteLine("4. Ver cuentas de tercero");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                CrearCuentadeTerceros();
                break;
            case "2":
                EliminarCuentadeTerceros();
                break;
            case "3":
                ActualizarCuentadeTerceros();
                break;
            case "4":
                MostrarCuentadeTerceros();
                break;
            default:
                Console.WriteLine("Opción inválida. Por favor, intente de nuevo.");
                break;
        }
    }

    private void CrearCuentadeTerceros()
    {
        Console.WriteLine("Ingrese el nombre del cuentahabitante");
        string nombre = Console.ReadLine(); // No need for validation, accepting any type of character

        string Numerodecuenta;

        while (true)
        {
            Console.WriteLine("Ingrese el número de cuenta:");
            Numerodecuenta = Console.ReadLine();

            if (thirdPartyAccounts.Any(a => a.Numerodecuenta == Numerodecuenta))
            {
                Console.WriteLine("El número de cuenta ya está en uso. Por favor, ingrese otro número de cuenta.");
            }
            else
            {
                break;
            }
        }

        string Nombrebanco = GetValidSalidaAlfabetica("Ingrese el nombre del banco:");

        int tipoCuentaSeleccionado;
        while (true)
        {
            Console.WriteLine("Ingrese el tipo de cuenta (1. Quetzales, 2. Dólares):");
            string tipoCuentaInput = Console.ReadLine();
            if (tipoCuentaInput == "1" || tipoCuentaInput == "2")
            {
                tipoCuentaSeleccionado = int.Parse(tipoCuentaInput);
                break;
            }
            else
            {
                Console.WriteLine("Opción inválida. Por favor, ingrese 1 para Quetzales o 2 para Dólares.");
            }
        }

        string tipoCuenta = tipoCuentaSeleccionado == 1 ? "Quetzales" : "Dólares";

        Console.WriteLine($"Se ha creado una cuenta de tercero con tipo de cuenta: {tipoCuenta}");

        Console.WriteLine("Ingrese la cantidad inicial:");
        decimal saldoInicial = decimal.Parse(Console.ReadLine());
        saldoInicial = RedondearsiTodoesCero(saldoInicial);
        ThirdPartyAccount newAccount = new ThirdPartyAccount(thirdPartyAccountIdCounter, nombre, Numerodecuenta, Nombrebanco, tipoCuenta, saldoInicial);
        thirdPartyAccounts.Add(newAccount);
        thirdPartyAccountIdCounter++;

        Console.WriteLine("Cuenta de tercero creada exitosamente.");
    }

    private void EliminarCuentadeTerceros()
    {
        Console.WriteLine("Ingrese el número de cuenta de la cuenta de tercero que desea eliminar:");
        string numeroCuenta = Console.ReadLine();

        ThirdPartyAccount account = thirdPartyAccounts.Find(a => a.Numerodecuenta == numeroCuenta);
        if (account != null)
        {
            thirdPartyAccounts.Remove(account);
            Console.WriteLine("Cuenta de tercero eliminada exitosamente.");
        }
        else
        {
            Console.WriteLine("Número de cuenta de tercero no válido.");
        }
    }

    private void ActualizarCuentadeTerceros()
    {
        Console.WriteLine("Ingrese el nombre del cuentahabitante");
        string nombre = Console.ReadLine(); // No need for validation, accepting any type of character

        Console.WriteLine("Ingrese el número de cuenta de la cuenta de tercero que desea actualizar:");
        string numeroCuenta = Console.ReadLine();

        ThirdPartyAccount account = thirdPartyAccounts.Find(a => a.Numerodecuenta == numeroCuenta);
        if (account != null)
        {
            Console.WriteLine("Ingrese el nuevo número de cuenta:");
            string Numerodecuenta = Console.ReadLine();
            string Nombrebanco = GetValidSalidaAlfabetica("Ingrese el nuevo nombre del banco:");
            Console.WriteLine("Ingrese el nuevo tipo de cuenta (Quetzales/Dólares):");
            string tipodecuenta = Console.ReadLine();

            account.Nombre = nombre;
            account.Numerodecuenta = Numerodecuenta;
            account.Nombrebanco = Nombrebanco;
            account.tipodecuenta = tipodecuenta;

            Console.WriteLine("Cuenta de tercero actualizada exitosamente.");
        }
        else
        {
            Console.WriteLine("Número de cuenta de tercero no válido.");
        }
    }

    private void MostrarCuentadeTerceros()
    {
        {
            Console.WriteLine("Cuentas de Tercero:");
            foreach (var account in thirdPartyAccounts)
            {
                string tipoCuenta = account.tipodecuenta == "Quetzales" ? "Quetzales" : account.tipodecuenta == "Dólares" ? "Dólares" : "Desconocido";
                Console.WriteLine($"ID: {account.ID}, Nombre: {account.Nombre}, Número de cuenta: {account.Numerodecuenta}, Nombre del banco: {account.Nombrebanco}, Tipo de cuenta: {tipoCuenta}, Saldo: Q{account.Saldo}" + (account.tipodecuenta == "Dólares" ? " ($" + (account.Saldo * 8.3m).ToString("0.00") + ")" : ""));
            }
        }
    }
    public void RealizarTransferencia()
    {
        Console.WriteLine("Ingrese el número de cuenta de destino:");
        string numeroCuentaDestino = Console.ReadLine();

        // Validar que el número de cuenta de destino sea válido
        ThirdPartyAccount account = thirdPartyAccounts.Find(a => a.Numerodecuenta == numeroCuentaDestino);
        if (account == null)
        {
            Console.WriteLine("Número de cuenta de destino no válido.");
            return;
        }
        Console.WriteLine("Ingrese la cantidad a transferir:");
        decimal cantidad = decimal.Parse(Console.ReadLine());
        if (BALANCE >= cantidad)
        {
            BALANCE -= cantidad;
            Console.WriteLine($"Se ha realizado la transferencia. Saldo actual: Q{BALANCE}");
            account.Saldo += cantidad;
            Console.WriteLine($"Se ha transferido Q{cantidad} a la cuenta de tercero. Saldo actual: Q{account.Saldo}");

            transactions.Add(new Transaction(DateTime.Now, cantidad, "Débito", $"Transferencia a cuenta {numeroCuentaDestino}"));
        }
        else
        {
            Console.WriteLine("Saldo insuficiente para realizar la transferencia.");
        }
    }

    public void PagarServicios()
    {
        Console.WriteLine("Ingrese el servicio a pagar:");
        string servicio = Console.ReadLine();
        Console.WriteLine("Ingrese la cantidad a pagar:");
        decimal cantidad = decimal.Parse(Console.ReadLine());

        if (BALANCE >= cantidad)
        {
            BALANCE -= cantidad;
            Console.WriteLine($"Se ha realizado el pago del servicio {servicio}. Saldo actual: Q{BALANCE}");
            transactions.Add(new Transaction(DateTime.Now, cantidad, "Débito", $"Pago de servicio {servicio}"));
        }
        else
        {
            Console.WriteLine("Saldo insuficiente para realizar el pago del servicio.");
        }
    }

    public void ImprimirReporteTransaccion()
    {
        Console.WriteLine("Reporte de Transacciones:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
    }

    public static decimal RedondearsiTodoesCero(decimal value)
    {
        decimal remainder = value % 1;
        if (remainder == 0)
        {
            return Math.Round(value, 2);
        }
        return value;
    }

    public static string GetValidSalidaAlfabetica(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
            {
                return input;
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese solo caracteres alfabéticos.");
            }
        }
    }
}

class Transaction
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }

    public Transaction(DateTime date, decimal amount, string type, string description)
    {
        Date = date;
        Amount = amount;
        Type = type;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} - {Type}: Q{Amount} - {Description}";
    }
}

class ThirdPartyAccount
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Numerodecuenta { get; set; }
    public string Nombrebanco { get; set; }
    public string tipodecuenta { get; set; }
    public decimal Saldo { get; set; }
    public string TipoCuenta { get; set; }

    public string ObtenerTipoCuentaYSaldoFormateado(int tipoCuentaSeleccionado)
    {
        string tipoCuenta = tipoCuentaSeleccionado == 1 ? "quetzales" : tipoCuentaSeleccionado == 2 ? "dólares" : "desconocido";
        return $"Tipo de cuenta: {tipoCuenta}, Saldo: Q{Saldo}";
    }

    public string ObtenerTipoCuentaYSaldo()
    {
        string tipoCuenta = tipodecuenta == "Quetzales" ? "Quetzales" : tipodecuenta == "Dólares" ? "Dólares" : "";
        return $"Tipo de cuenta: {tipoCuenta}, Saldo: Q{Saldo}" + (tipoCuenta == "Dólares" ? " ($" + (Saldo * 8.3m).ToString("0.00") + ")" : "");
    }
    public ThirdPartyAccount(int id, string nombre, string numerodecuenta, string nombrebanco, string tipodecuenta, decimal saldo)
    {
        ID = id;
        Nombre = nombre;
        Numerodecuenta = numerodecuenta;
        Nombrebanco = nombrebanco;
        tipodecuenta = tipodecuenta;
        Saldo = saldo;
    }
}
