#region IsExternalInit

class InitExample
{
    private int member;

    public int Member
    {
        get => member;
        init => member = value;
    }
}

#endregion