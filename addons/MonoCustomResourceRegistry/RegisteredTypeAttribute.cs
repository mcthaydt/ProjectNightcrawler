using Godot;
using System;

namespace MonoCustomResourceRegistry
{
	[AttributeUsage(System.AttributeTargets.Class)]
	public partial class RegisteredTypeAttribute : System.Attribute
	{
		public string Name;
		public string IconPath;
		public string BaseType;

		public RegisteredTypeAttribute(string name, string iconPath = "", string baseType = "")
		{
			this.Name = name;
			this.IconPath = iconPath;
			this.BaseType = baseType;
		}
	}
}