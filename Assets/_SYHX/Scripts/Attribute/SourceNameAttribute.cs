using System;
/*
 *用于做显示
 */
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class SourceNameAttribute : Attribute
{
    public readonly string desc;
    public SourceNameAttribute(string desc)
    {
        this.desc = desc;
    }
}