using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Metadata;

class MoneyTransfer
{
    public int Amount { get; }
    public string Recipient{ get; }
    public MoneyTransfer(int amount, string recipient)
    {
        Amount = amount;
        Recipient = recipient;
    }
}
abstract class TransferHandler 
{
    protected TransferHandler nextHandler;
    public void SetNextHandler(TransferHandler handler)
    {
        nextHandler = handler;
    }
    public abstract void HandleTransfer(MoneyTransfer transfer);


}
class BankTransfer : TransferHandler
{
    public override void HandleTransfer(MoneyTransfer transfer)
    {
        Console.WriteLine($"bank transfer is done: {transfer.Amount} uah recipient: {transfer.Recipient}");
    }
}
class WesternUnion : TransferHandler
{
    public override void HandleTransfer(MoneyTransfer transfer)
    {
        Console.WriteLine($"Western Union transfer is done: {transfer.Amount} uah recipient: {transfer.Recipient}");
    }
}
class Unistream : TransferHandler
{
    public override void HandleTransfer(MoneyTransfer transfer)
    {
        Console.WriteLine($"Unistream transfer is done: {transfer.Amount} uah recipient: {transfer.Recipient}");
    }
}
class PayPal : TransferHandler
{
    public override void HandleTransfer(MoneyTransfer transfer)
    {
        Console.WriteLine($"PayPal transfer is done: {transfer.Amount} uah recipient: {transfer.Recipient}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        var bankTransfer = new BankTransfer();
        var westernUnionTransfer = new WesternUnion();
        var unistreamTransfer = new Unistream();
        var payPalTransfer = new PayPal();

        bankTransfer.SetNextHandler(westernUnionTransfer);
        westernUnionTransfer.SetNextHandler(unistreamTransfer);
        unistreamTransfer.SetNextHandler(payPalTransfer);

        var transfer = new MoneyTransfer(20000, "andrew");
        bankTransfer.HandleTransfer(transfer);

    }
}
