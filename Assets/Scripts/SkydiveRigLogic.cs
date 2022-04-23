public class SkydiveRigLogic : OwnedItem
{
    public OwnedItem Container;

    public OwnedItem Main;

    public OwnedItem Reserve;

    public OwnedItem Aad;

    public OwnedItem RigAccessory;

    public SkydiveRigLogic(OwnedItem container, OwnershipSpecs ownershipSpecs, OwnedItem main,OwnedItem reserve,OwnedItem aad) : base(container.TypeOfOwnable, ownershipSpecs)
    {
        this.Container = container;
        this.Main = main;
        this.Reserve = reserve;
        this.Aad = aad;

    }

    public void SetupSkydiveRigLogic(OwnedItem _container, OwnedItem _main, OwnedItem _reserve, OwnedItem _rigAccessory)
    {
        Container = _container;
        Main = _main;
        Reserve = _reserve;
        RigAccessory = _rigAccessory;
    }

    public new int GetMoneyValue()
    {
        
        return Container.GetMoneyValue() + Main.GetMoneyValue() + Reserve.GetMoneyValue() + Aad.GetMoneyValue() + RigAccessory.GetMoneyValue();
    }


}


