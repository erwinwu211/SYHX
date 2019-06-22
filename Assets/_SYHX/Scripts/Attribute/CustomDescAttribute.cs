using System;
/*
 *用于做描述
 */
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
sealed class CustomDescAttribute : Attribute
{
    public readonly string descName;
    public CustomDescAttribute(string descName)
    {
        this.descName = descName;
    }
}
