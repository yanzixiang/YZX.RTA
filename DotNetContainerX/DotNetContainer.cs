using Greg.WPF.Utility;
using Siemens.Runtime;
using Siemens.Runtime.ITag;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FwDotNetContainer
{
  [ComDefaultInterface(typeof (IDotNetContainer))]
  [ClassInterface(ClassInterfaceType.None)]
  [Guid("B5D12176-4796-48e9-AEDB-2D107A360993")]
  [ProgId("FwDotNetContainer.DotNetContainer")]
  [ComVisible(true)]
  [ComSourceInterfaces(typeof (IDotNetContainerEvents))]
  public class DotNetContainer : UserControl, IDotNetContainer, IPersistStreamManaged, IConnectionPointContainer, System.Reflection.IReflect, ISite, IServiceProvider, IRuntimeContext
  {
    public static EventHelper<IDotNetContainerEvents> _eventHelper = null;
    private static Hashtable _deviceOptions = new Hashtable();
    private Control _control = null;
    public EventContainerHelper _eventContainer = null;
    private IContainer components = null;
    private ComponentData _componentData = null;
    private Assembly _assembly = null;
    private Type _type = null;
    private string _errorText;

    public string ErrorText
    {
      get
      {
        return this._errorText;
      }
    }

    public Type UnderlyingSystemType
    {
      get
      {
        return this._control.GetType().UnderlyingSystemType;
      }
    }

    public IComponent Component
    {
      get
      {
        return this.Site.Component;
      }
    }

    public new IContainer Container
    {
      get
      {
        if(this.Site != null)
        {
          return this.Site.Container;
        }
        else
        {
          return null;
        }
      }
    }

    public new bool DesignMode
    {
      get
      {
        if (this.Site != null)
        {
          return this.Site.DesignMode;
        }
        else
        {
          return false;
        }
      }
    }

    public string DeviceType
    {
      get
      {
        return "WinCC RT Advanced";
      }
    }

    public IDictionary<string, object> DeviceOptions
    {
      get
      {
        return (IDictionary<string, object>)_deviceOptions;
      }
    }

    event EventHandler<DeviceOptionChangedEventArgs> IRuntimeContext.DeviceOptionChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public DotNetContainer()
    {
      Debugger.Launch();
      this.InitializeComponent();
      //this._eventContainer = new EventContainerHelper(this);
      //_eventHelper = this._eventContainer.AddEvents<IDotNetContainerEvents>();

      Click += DotNetContainer_Click;
    }

    private void DotNetContainer_Click(object sender, EventArgs e)
    {
      MessageBox.Show("DotNetContainer_Click");
    }

    protected override void OnCreateControl()
    {
      if (this._control == null)
        return;
      this._control.Width = this.Width;
      this._control.Height = this.Height;
    }

    public void LoadStream(Stream stream)
    {
      try
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (ComponentData));
        ComponentData componentData;
        try
        {
          Byte[] bytes = new Byte[stream.Length];
          stream.Read(bytes, 0, (int)stream.Length);
          MemoryStream ms1 = new MemoryStream(bytes);
          StreamReader sr = new StreamReader(ms1);
          String s = sr.ReadToEnd();

          stream.Seek(0, SeekOrigin.Begin);
          componentData = (ComponentData) xmlSerializer.Deserialize(stream);
        }
        catch
        {
          this._errorText += "\n Error in serialization format";
          throw;
        }
        this.InitData(componentData);
      }
      catch
      {
        this.Controls.Clear();
        this._errorText += "\n Invalid initialization data \n";
        this.CreateErrorLabel();
      }
    }

    private void CreateErrorLabel()
    {
      System.Windows.Forms.Label label = new System.Windows.Forms.Label();
      label.ForeColor = Color.Red;
      this.BackColor = Color.White;
      label.Text = this._errorText;
      this.Controls.Add(label);
      label.AutoSize = true;
      label.Show();
    }

    public void LoadFrom(string text, bool isFilePath)
    {
      if (isFilePath)
      {
        FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read);
        this.LoadStream(fileStream);
        fileStream.Close();
      }
      else
      {
        MemoryStream memoryStream = new MemoryStream();
        byte[] bytes = Encoding.ASCII.GetBytes(text);
        memoryStream.Write(bytes, 0, bytes.Length);
        this.LoadStream(memoryStream);
        memoryStream.Close();
      }
    }

    [ComRegisterFunction]
    public static void SelfRegister(string givenKey)
    {
    }

    [ComUnregisterFunction]
    public static void SelfUnregister(string givenKey)
    {
    }

    public FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      return this.GetType().GetField(name, bindingAttr);
    }

    public FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      return this.GetType().GetFields(bindingAttr);
    }

    public MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
    {
      return this.GetType().GetMember(name, bindingAttr);
    }

    public MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      return this.GetType().GetMembers(bindingAttr);
    }

    public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
    {
      return this.GetType().GetMethod(name, bindingAttr);
    }

    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
    {
      return this.GetType().GetMethod(name, bindingAttr, binder, types, modifiers);
    }

    public MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      return this.GetType().GetMethods(bindingAttr);
    }

    public PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      return this.GetType().GetProperties(bindingAttr);
    }

    public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      return this.GetType().GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
    }

    public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
    {
      return this.GetType().GetProperty(name, bindingAttr);
    }

    public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      Type type = this.GetType();
      int num = 0;
      if (name.ToLower().StartsWith("[dispid="))
      {
        string str = name.Split('=')[1];
        num = int.Parse(str.Substring(0, str.Length - 1));
      }
      foreach (ComponentProperty componentProperty in this._componentData.Properties)
      {
        if (componentProperty.DispID == num)
        {
          PropertyInfo property = this._control.GetType().GetProperty(componentProperty.Name);
          if (property != null)
            property.SetValue(this._control, args[0], null);
        }
      }
      return type.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
    }

    public void EnumConnectionPoints(out IEnumConnectionPoints ppEnum)
    {
      this._eventContainer.EnumConnectionPoints(out ppEnum);
    }

    public void FindConnectionPoint(ref Guid riid, out IConnectionPoint ppCP)
    {
      this._eventContainer.FindConnectionPoint(ref riid, out ppCP);
    }

    public int LoadStream(IStream stream)
    {
      ComStream comStream = new ComStream(stream);
      try
      {
        this.LoadStream(comStream);
      }
      catch
      {
      }
      comStream.Close();
      Marshal.ReleaseComObject(stream);
      return 1;
    }

    public new  object GetService(Type serviceType)
    {
      if (serviceType == typeof (IRuntimeContext))
        return this;
      if (serviceType == typeof (ITag))
        return Activator.CreateInstance(Type.GetTypeFromProgID("SimaticHMI.CoRtTagDataProvider.TagDataProvider"));
      return this.Site.GetService(serviceType);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = "UserControl1";
      this.Size = new Size(241, 138);
      this.ResumeLayout(false);
    }

    private void InitData(ComponentData componentData)
    {
      this._componentData = componentData;
      this.Controls.Clear();
      try
      {
        if (!string.IsNullOrEmpty(this._componentData.Assembly))
        {
          AssemblyName an = new AssemblyName(this._componentData.Assembly);
          this._assembly = Assembly.Load(an);
        }
        else
        {
          if (string.IsNullOrEmpty(this._componentData.CodeBase))
            throw new ArgumentException("Either the CodeBase or Assembly should be specified");
          if (!Path.IsPathRooted(this._componentData.CodeBase))
          {
            this._componentData.CodeBase = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), this._componentData.CodeBase);
            this._componentData.CodeBase = this._componentData.CodeBase.Replace("file:\\", "");
          }
          this._assembly = Assembly.LoadFile(this._componentData.CodeBase);
        }
        Type[] types =  this._assembly.GetTypes();
        this._type = this._assembly.GetType(this._componentData.Type);
        if (this._type == null)
          throw new NullReferenceException("Type '" + this._componentData.Type + "' not found in the loaded assembly");
        this._control = (Control) Activator.CreateInstance(this._type);
        if (this._control == null)
          throw new NullReferenceException("Cannot create instance of Type '" + this._componentData.Type + "'");
        this._control.Site = this;
        if (typeof (ContainerControl).IsInstanceOfType(this._control))
          this._control.TabStop = false;
        this.Controls.Add(this._control);
        this.SetupProperties();
        this.SetupEvents();
      }
      catch(Exception ex)
      {
        ExceptionMessageBox emb = new ExceptionMessageBox(ex, "InitData");
        emb.Show();
        this._errorText += "\n Exception occured while initializing data for contained control \n" + ex.ToString();
        throw;
      }
    }

    private void SetupProperties()
    {
      try
      {
        ColorConverter colorConverter = new ColorConverter();
        foreach (ComponentProperty componentProperty in this._componentData.Properties)
        {
          if (componentProperty.value != null)
          {
            PropertyInfo property = this._type.GetProperty(componentProperty.Name);
            if (property == null)
            {
              DotNetContainer dotNetContainer = this;
              string str = dotNetContainer._errorText + "\n Property '" + componentProperty.Name + "' not found";
              dotNetContainer._errorText = str;
              throw new ArgumentException();
            }
            object obj = !string.IsNullOrEmpty(componentProperty.value) ? (!property.PropertyType.IsEnum ? (!(property.PropertyType.Name == "Color") ? (!(property.PropertyType == typeof(string)) ? Convert.ChangeType(componentProperty.ObjectValue, property.PropertyType) : (object)componentProperty.value) : colorConverter.ConvertFromString(componentProperty.value)) : Enum.Parse(property.PropertyType, componentProperty.value)) : componentProperty.ObjectValue;
            property.SetValue(this._control, obj, null);
          }
        }
      }
      catch
      {
        this._errorText += "\n Exception occured while initializing properties";
        throw;
      }
    }

    private void SetupEvents()
    {
      int num = 0;
      foreach (ComponentEvent events in this._componentData.Events)
      {
        if (this.GenerateDynamicEventHandler(string.Concat(new object[4]
        {
          "Handler_",
          events.Name,
          "_",
          num++
        }), events) == null)
          break;
      }
    }

    private Type ConvertType(ParameterInfo info)
    {
      return info.ParameterType;
    }

    private DynamicMethod GenerateDynamicEventHandler(
      string eventHandlerName, 
      ComponentEvent events)
    {
      EventInfo @event = this._type.GetEvent(events.Name);
      Type type = Type.GetType(events.Type);
      if (type == null)
        type = this._assembly.GetType(events.Type);
      if (type.BaseType != typeof (MulticastDelegate))
        throw new InvalidOperationException("The event type is not a delegate");
      MethodInfo method1 = type.GetMethod("Invoke");
      if (method1 == null)
        throw new InvalidOperationException("The event type is not a delegate");
      Type[] parameterTypes = Array.ConvertAll(method1.GetParameters(), new Converter<ParameterInfo, Type>(this.ConvertType));
      DynamicMethod method2 = new DynamicMethod(eventHandlerName, method1.ReturnType, parameterTypes, typeof (DotNetContainer).Module);
      try
      {
        this.GenerateMethodBody(method2, method1.ReturnType, parameterTypes, int.Parse(events.RuntimeAction));
      }
      catch (FormatException ex)
      {
      }
      @event.AddEventHandler(this._control, method2.CreateDelegate(type));
      return method2;
    }

    private void GenerateMethodBody(DynamicMethod method, Type returnType, Type[] parameterTypes, int rtAction)
    {
      MethodInfo method1 = this.GetType().GetMethod("CallByDispIDIndirect", new Type[1]
      {
        typeof (int)
      });
      ILGenerator ilGenerator = method.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Ldc_I4, rtAction);
      ilGenerator.EmitCall(OpCodes.Callvirt, method1, null);
      ilGenerator.Emit(OpCodes.Ret);
    }

    public void CallByDispIDIndirect(int dispId)
    {
      foreach (object target in _eventHelper.EventObservers.Connections)
      {
        try
        {
          target.GetType().InvokeMember("[DispId=" + dispId + "]", BindingFlags.InvokeMethod, null, target, null);
        }
        catch (Exception ex)
        {
        }
      }
    }

    public static string GetPropertyValueAsString(object value)
    {
      MemoryStream memoryStream = new MemoryStream();
      new BinaryFormatter().Serialize(memoryStream, value);
      byte[] numArray = new byte[memoryStream.Length];
      memoryStream.Seek(0L, SeekOrigin.Begin);
      memoryStream.Read(numArray, 0, (int) memoryStream.Length);
      return Convert.ToBase64String(numArray);
    }

    public static object GetPropertyValueAsObject(string propValue)
    {
      byte[] buffer = Convert.FromBase64String(propValue);
      MemoryStream memoryStream = new MemoryStream(buffer);
      memoryStream.SetLength(buffer.Length);
      return new BinaryFormatter().Deserialize(memoryStream);
    }
  }
}
