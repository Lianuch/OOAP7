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
    public virtual void HandleTransfer(MoneyTransfer transfer)
    {
        this.nextHandler?.HandleTransfer(transfer);
    }


}
class BankTransfer : TransferHandler
{
    public override void HandleTransfer(MoneyTransfer transfer)
    {
        if(transfer.Amount<200)
            Console.WriteLine($"bank transfer is done: {transfer.Amount} uah recipient: {transfer.Recipient}");
        else
            base.HandleTransfer(transfer);
            
    }
}
class WesternUnion : TransferHandler
{
    public override void HandleTransfer(MoneyTransfer transfer)
    {
        if (transfer.Amount < 2000)
            Console.WriteLine($"Western Union transfer is done: {transfer.Amount} uah recipient: {transfer.Recipient}");
        else
            base.HandleTransfer(transfer);
    }
}
class Unistream : TransferHandler
{
    public override void HandleTransfer(MoneyTransfer transfer)
    {
        if (transfer.Amount < 10000)
            Console.WriteLine($"Unistream transfer is done: {transfer.Amount} uah recipient: {transfer.Recipient}");
        else
            base.HandleTransfer(transfer);
    }
}
class PayPal : TransferHandler
{
    public override void HandleTransfer(MoneyTransfer transfer)
    {
        if (transfer.Amount < 50000)
            Console.WriteLine($"PayPal transfer is done: {transfer.Amount} uah recipient: {transfer.Recipient}");
        else
            base.HandleTransfer(transfer);
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

        var transfer = new MoneyTransfer(100000, "andrew");
        bankTransfer.HandleTransfer(transfer);

    }
}
