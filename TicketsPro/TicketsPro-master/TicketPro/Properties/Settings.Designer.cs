namespace TicketPro.Properties {
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.14.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=PCDEJUAN\\SQLDEVELOPER;Initial Catalog=ProyectoTicketsPro;Integrated S" +
            "ecurity=True;TrustServerCertificate=True")]
        public string ProyectoTicketsProConnectionString {
            get {
                return ((string)(this["ProyectoTicketsProConnectionString"]));
            }
        }
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=PCDEJUAN\\SQLDEVELOPER;Initial Catalog=TicketsPro;Integrated Security=" +
            "True;TrustServerCertificate=True")]
        public string TicketsProConnectionString {
            get {
                return ((string)(this["TicketsProConnectionString"]));
            }
        }
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=localhost\\SQLEXPRESS;Initial Catalog=TicketsProDB_v2;Integrated Secur" +
            "ity=True;Encrypt=False;TrustServerCertificate=True")]
        public string TicketsProDB_v2ConnectionString {
            get {
                return ((string)(this["TicketsProDB_v2ConnectionString"]));
            }
        }
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=localhost\\SQLEXPRESS;Initial Catalog=ServicesPP;Integrated Security=T" +
            "rue;TrustServerCertificate=True")]
        public string ServicesPPConnectionString {
            get {
                return ((string)(this["ServicesPPConnectionString"]));
            }
        }
    }
}
