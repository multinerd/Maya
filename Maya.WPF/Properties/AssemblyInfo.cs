﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Maya.WPF")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Maya.WPF")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("ae79ed59-ed65-48c6-ba94-15826195ddfb")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]





[assembly: XmlnsPrefix("http://schemas.multinerd.io/maya/converters/", "maya.Converters")]
[assembly: XmlnsDefinition("http://schemas.multinerd.io/maya/converters/", "Maya.WPF.Converters")]


[assembly: XmlnsPrefix("http://schemas.multinerd.io/maya/attached_behavior/", "maya.Attached_Behavior")]
[assembly: XmlnsDefinition("http://schemas.multinerd.io/maya/attached_behavior/", "Maya.WPF.Attached_Behavior")]

//[assembly: XmlnsDefinition("http://schemas.multinerd.io/maya/", "Maya.WPF.Helpers")]
//[assembly: XmlnsDefinition("http://schemas.multinerd.io/maya/", "Maya.WPF.Models")]