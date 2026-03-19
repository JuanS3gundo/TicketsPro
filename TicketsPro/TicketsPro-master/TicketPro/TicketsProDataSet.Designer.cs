#pragma warning disable 1591
namespace TicketPro {
    [global::System.Serializable()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [global::System.Xml.Serialization.XmlRootAttribute("TicketsProDataSet")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class TicketsProDataSet : global::System.Data.DataSet {
        private EquipoInformaticoDataTable tableEquipoInformatico;
        private SolucionTicketDataTable tableSolucionTicket;
        private TecnicoDataTable tableTecnico;
        private TicketDataTable tableTicket;
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public TicketsProDataSet() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected TicketsProDataSet(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                if ((ds.Tables["EquipoInformatico"] != null)) {
                    base.Tables.Add(new EquipoInformaticoDataTable(ds.Tables["EquipoInformatico"]));
                }
                if ((ds.Tables["SolucionTicket"] != null)) {
                    base.Tables.Add(new SolucionTicketDataTable(ds.Tables["SolucionTicket"]));
                }
                if ((ds.Tables["Tecnico"] != null)) {
                    base.Tables.Add(new TecnicoDataTable(ds.Tables["Tecnico"]));
                }
                if ((ds.Tables["Ticket"] != null)) {
                    base.Tables.Add(new TicketDataTable(ds.Tables["Ticket"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public EquipoInformaticoDataTable EquipoInformatico {
            get {
                return this.tableEquipoInformatico;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public SolucionTicketDataTable SolucionTicket {
            get {
                return this.tableSolucionTicket;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public TecnicoDataTable Tecnico {
            get {
                return this.tableTecnico;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public TicketDataTable Ticket {
            get {
                return this.tableTicket;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.BrowsableAttribute(true)]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public override global::System.Data.DataSet Clone() {
            TicketsProDataSet cln = ((TicketsProDataSet)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["EquipoInformatico"] != null)) {
                    base.Tables.Add(new EquipoInformaticoDataTable(ds.Tables["EquipoInformatico"]));
                }
                if ((ds.Tables["SolucionTicket"] != null)) {
                    base.Tables.Add(new SolucionTicketDataTable(ds.Tables["SolucionTicket"]));
                }
                if ((ds.Tables["Tecnico"] != null)) {
                    base.Tables.Add(new TecnicoDataTable(ds.Tables["Tecnico"]));
                }
                if ((ds.Tables["Ticket"] != null)) {
                    base.Tables.Add(new TicketDataTable(ds.Tables["Ticket"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal void InitVars() {
            this.InitVars(true);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal void InitVars(bool initTable) {
            this.tableEquipoInformatico = ((EquipoInformaticoDataTable)(base.Tables["EquipoInformatico"]));
            if ((initTable == true)) {
                if ((this.tableEquipoInformatico != null)) {
                    this.tableEquipoInformatico.InitVars();
                }
            }
            this.tableSolucionTicket = ((SolucionTicketDataTable)(base.Tables["SolucionTicket"]));
            if ((initTable == true)) {
                if ((this.tableSolucionTicket != null)) {
                    this.tableSolucionTicket.InitVars();
                }
            }
            this.tableTecnico = ((TecnicoDataTable)(base.Tables["Tecnico"]));
            if ((initTable == true)) {
                if ((this.tableTecnico != null)) {
                    this.tableTecnico.InitVars();
                }
            }
            this.tableTicket = ((TicketDataTable)(base.Tables["Ticket"]));
            if ((initTable == true)) {
                if ((this.tableTicket != null)) {
                    this.tableTicket.InitVars();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitClass() {
            this.DataSetName = "TicketsProDataSet";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/TicketsProDataSet.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableEquipoInformatico = new EquipoInformaticoDataTable();
            base.Tables.Add(this.tableEquipoInformatico);
            this.tableSolucionTicket = new SolucionTicketDataTable();
            base.Tables.Add(this.tableSolucionTicket);
            this.tableTecnico = new TecnicoDataTable();
            base.Tables.Add(this.tableTecnico);
            this.tableTicket = new TicketDataTable();
            base.Tables.Add(this.tableTicket);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private bool ShouldSerializeEquipoInformatico() {
            return false;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private bool ShouldSerializeSolucionTicket() {
            return false;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private bool ShouldSerializeTecnico() {
            return false;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private bool ShouldSerializeTicket() {
            return false;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
            TicketsProDataSet ds = new TicketsProDataSet();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace)) {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length)) {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length) 
                                        && (s1.ReadByte() == s2.ReadByte())); ) {
                                ;
                            }
                            if ((s1.Position == s1.Length)) {
                                return type;
                            }
                        }
                    }
                }
                finally {
                    if ((s1 != null)) {
                        s1.Close();
                    }
                    if ((s2 != null)) {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public delegate void EquipoInformaticoRowChangeEventHandler(object sender, EquipoInformaticoRowChangeEvent e);
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public delegate void SolucionTicketRowChangeEventHandler(object sender, SolucionTicketRowChangeEvent e);
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public delegate void TecnicoRowChangeEventHandler(object sender, TecnicoRowChangeEvent e);
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public delegate void TicketRowChangeEventHandler(object sender, TicketRowChangeEvent e);
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class EquipoInformaticoDataTable : global::System.Data.TypedTableBase<EquipoInformaticoRow> {
            private global::System.Data.DataColumn columnId;
            private global::System.Data.DataColumn columnModeloEquipo;
            private global::System.Data.DataColumn columnNroInventario;
            private global::System.Data.DataColumn columnProcesador;
            private global::System.Data.DataColumn columnRAM;
            private global::System.Data.DataColumn columnROM;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public EquipoInformaticoDataTable() {
                this.TableName = "EquipoInformatico";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal EquipoInformaticoDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected EquipoInformaticoDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn IdColumn {
                get {
                    return this.columnId;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn ModeloEquipoColumn {
                get {
                    return this.columnModeloEquipo;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn NroInventarioColumn {
                get {
                    return this.columnNroInventario;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn ProcesadorColumn {
                get {
                    return this.columnProcesador;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn RAMColumn {
                get {
                    return this.columnRAM;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn ROMColumn {
                get {
                    return this.columnROM;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public EquipoInformaticoRow this[int index] {
                get {
                    return ((EquipoInformaticoRow)(this.Rows[index]));
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event EquipoInformaticoRowChangeEventHandler EquipoInformaticoRowChanging;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event EquipoInformaticoRowChangeEventHandler EquipoInformaticoRowChanged;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event EquipoInformaticoRowChangeEventHandler EquipoInformaticoRowDeleting;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event EquipoInformaticoRowChangeEventHandler EquipoInformaticoRowDeleted;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public void AddEquipoInformaticoRow(EquipoInformaticoRow row) {
                this.Rows.Add(row);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public EquipoInformaticoRow AddEquipoInformaticoRow(string ModeloEquipo, string NroInventario, string Procesador, int RAM, int ROM) {
                EquipoInformaticoRow rowEquipoInformaticoRow = ((EquipoInformaticoRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        null,
                        ModeloEquipo,
                        NroInventario,
                        Procesador,
                        RAM,
                        ROM};
                rowEquipoInformaticoRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowEquipoInformaticoRow);
                return rowEquipoInformaticoRow;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public EquipoInformaticoRow FindById(int Id) {
                return ((EquipoInformaticoRow)(this.Rows.Find(new object[] {
                            Id})));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public override global::System.Data.DataTable Clone() {
                EquipoInformaticoDataTable cln = ((EquipoInformaticoDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Data.DataTable CreateInstance() {
                return new EquipoInformaticoDataTable();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal void InitVars() {
                this.columnId = base.Columns["Id"];
                this.columnModeloEquipo = base.Columns["ModeloEquipo"];
                this.columnNroInventario = base.Columns["NroInventario"];
                this.columnProcesador = base.Columns["Procesador"];
                this.columnRAM = base.Columns["RAM"];
                this.columnROM = base.Columns["ROM"];
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            private void InitClass() {
                this.columnId = new global::System.Data.DataColumn("Id", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnId);
                this.columnModeloEquipo = new global::System.Data.DataColumn("ModeloEquipo", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnModeloEquipo);
                this.columnNroInventario = new global::System.Data.DataColumn("NroInventario", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnNroInventario);
                this.columnProcesador = new global::System.Data.DataColumn("Procesador", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnProcesador);
                this.columnRAM = new global::System.Data.DataColumn("RAM", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnRAM);
                this.columnROM = new global::System.Data.DataColumn("ROM", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnROM);
                this.Constraints.Add(new global::System.Data.UniqueConstraint("Constraint1", new global::System.Data.DataColumn[] {
                                this.columnId}, true));
                this.columnId.AutoIncrement = true;
                this.columnId.AutoIncrementSeed = -1;
                this.columnId.AutoIncrementStep = -1;
                this.columnId.AllowDBNull = false;
                this.columnId.ReadOnly = true;
                this.columnId.Unique = true;
                this.columnModeloEquipo.AllowDBNull = false;
                this.columnModeloEquipo.MaxLength = 50;
                this.columnNroInventario.AllowDBNull = false;
                this.columnNroInventario.MaxLength = 50;
                this.columnProcesador.AllowDBNull = false;
                this.columnProcesador.MaxLength = 50;
                this.columnRAM.AllowDBNull = false;
                this.columnROM.AllowDBNull = false;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public EquipoInformaticoRow NewEquipoInformaticoRow() {
                return ((EquipoInformaticoRow)(this.NewRow()));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new EquipoInformaticoRow(builder);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Type GetRowType() {
                return typeof(EquipoInformaticoRow);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.EquipoInformaticoRowChanged != null)) {
                    this.EquipoInformaticoRowChanged(this, new EquipoInformaticoRowChangeEvent(((EquipoInformaticoRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.EquipoInformaticoRowChanging != null)) {
                    this.EquipoInformaticoRowChanging(this, new EquipoInformaticoRowChangeEvent(((EquipoInformaticoRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.EquipoInformaticoRowDeleted != null)) {
                    this.EquipoInformaticoRowDeleted(this, new EquipoInformaticoRowChangeEvent(((EquipoInformaticoRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.EquipoInformaticoRowDeleting != null)) {
                    this.EquipoInformaticoRowDeleting(this, new EquipoInformaticoRowChangeEvent(((EquipoInformaticoRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public void RemoveEquipoInformaticoRow(EquipoInformaticoRow row) {
                this.Rows.Remove(row);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                TicketsProDataSet ds = new TicketsProDataSet();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "EquipoInformaticoDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class SolucionTicketDataTable : global::System.Data.TypedTableBase<SolucionTicketRow> {
            private global::System.Data.DataColumn columnId;
            private global::System.Data.DataColumn columnDescripcionSolucion;
            private global::System.Data.DataColumn columnFechaCierre;
            private global::System.Data.DataColumn columnTicketId;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public SolucionTicketDataTable() {
                this.TableName = "SolucionTicket";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal SolucionTicketDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected SolucionTicketDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn IdColumn {
                get {
                    return this.columnId;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn DescripcionSolucionColumn {
                get {
                    return this.columnDescripcionSolucion;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn FechaCierreColumn {
                get {
                    return this.columnFechaCierre;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn TicketIdColumn {
                get {
                    return this.columnTicketId;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public SolucionTicketRow this[int index] {
                get {
                    return ((SolucionTicketRow)(this.Rows[index]));
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event SolucionTicketRowChangeEventHandler SolucionTicketRowChanging;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event SolucionTicketRowChangeEventHandler SolucionTicketRowChanged;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event SolucionTicketRowChangeEventHandler SolucionTicketRowDeleting;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event SolucionTicketRowChangeEventHandler SolucionTicketRowDeleted;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public void AddSolucionTicketRow(SolucionTicketRow row) {
                this.Rows.Add(row);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public SolucionTicketRow AddSolucionTicketRow(string DescripcionSolucion, System.DateTime FechaCierre, int TicketId) {
                SolucionTicketRow rowSolucionTicketRow = ((SolucionTicketRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        null,
                        DescripcionSolucion,
                        FechaCierre,
                        TicketId};
                rowSolucionTicketRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowSolucionTicketRow);
                return rowSolucionTicketRow;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public SolucionTicketRow FindById(int Id) {
                return ((SolucionTicketRow)(this.Rows.Find(new object[] {
                            Id})));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public override global::System.Data.DataTable Clone() {
                SolucionTicketDataTable cln = ((SolucionTicketDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Data.DataTable CreateInstance() {
                return new SolucionTicketDataTable();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal void InitVars() {
                this.columnId = base.Columns["Id"];
                this.columnDescripcionSolucion = base.Columns["DescripcionSolucion"];
                this.columnFechaCierre = base.Columns["FechaCierre"];
                this.columnTicketId = base.Columns["TicketId"];
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            private void InitClass() {
                this.columnId = new global::System.Data.DataColumn("Id", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnId);
                this.columnDescripcionSolucion = new global::System.Data.DataColumn("DescripcionSolucion", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDescripcionSolucion);
                this.columnFechaCierre = new global::System.Data.DataColumn("FechaCierre", typeof(global::System.DateTime), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnFechaCierre);
                this.columnTicketId = new global::System.Data.DataColumn("TicketId", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnTicketId);
                this.Constraints.Add(new global::System.Data.UniqueConstraint("Constraint1", new global::System.Data.DataColumn[] {
                                this.columnId}, true));
                this.columnId.AutoIncrement = true;
                this.columnId.AutoIncrementSeed = -1;
                this.columnId.AutoIncrementStep = -1;
                this.columnId.AllowDBNull = false;
                this.columnId.ReadOnly = true;
                this.columnId.Unique = true;
                this.columnDescripcionSolucion.AllowDBNull = false;
                this.columnDescripcionSolucion.MaxLength = 50;
                this.columnFechaCierre.AllowDBNull = false;
                this.columnTicketId.AllowDBNull = false;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public SolucionTicketRow NewSolucionTicketRow() {
                return ((SolucionTicketRow)(this.NewRow()));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new SolucionTicketRow(builder);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Type GetRowType() {
                return typeof(SolucionTicketRow);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.SolucionTicketRowChanged != null)) {
                    this.SolucionTicketRowChanged(this, new SolucionTicketRowChangeEvent(((SolucionTicketRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.SolucionTicketRowChanging != null)) {
                    this.SolucionTicketRowChanging(this, new SolucionTicketRowChangeEvent(((SolucionTicketRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.SolucionTicketRowDeleted != null)) {
                    this.SolucionTicketRowDeleted(this, new SolucionTicketRowChangeEvent(((SolucionTicketRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.SolucionTicketRowDeleting != null)) {
                    this.SolucionTicketRowDeleting(this, new SolucionTicketRowChangeEvent(((SolucionTicketRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public void RemoveSolucionTicketRow(SolucionTicketRow row) {
                this.Rows.Remove(row);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                TicketsProDataSet ds = new TicketsProDataSet();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "SolucionTicketDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class TecnicoDataTable : global::System.Data.TypedTableBase<TecnicoRow> {
            private global::System.Data.DataColumn columnId;
            private global::System.Data.DataColumn columnLegajo;
            private global::System.Data.DataColumn columnMail;
            private global::System.Data.DataColumn columnNombreApellido;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TecnicoDataTable() {
                this.TableName = "Tecnico";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal TecnicoDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected TecnicoDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn IdColumn {
                get {
                    return this.columnId;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn LegajoColumn {
                get {
                    return this.columnLegajo;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn MailColumn {
                get {
                    return this.columnMail;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn NombreApellidoColumn {
                get {
                    return this.columnNombreApellido;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TecnicoRow this[int index] {
                get {
                    return ((TecnicoRow)(this.Rows[index]));
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event TecnicoRowChangeEventHandler TecnicoRowChanging;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event TecnicoRowChangeEventHandler TecnicoRowChanged;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event TecnicoRowChangeEventHandler TecnicoRowDeleting;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event TecnicoRowChangeEventHandler TecnicoRowDeleted;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public void AddTecnicoRow(TecnicoRow row) {
                this.Rows.Add(row);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TecnicoRow AddTecnicoRow(string Legajo, string Mail, string NombreApellido) {
                TecnicoRow rowTecnicoRow = ((TecnicoRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        null,
                        Legajo,
                        Mail,
                        NombreApellido};
                rowTecnicoRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowTecnicoRow);
                return rowTecnicoRow;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TecnicoRow FindById(int Id) {
                return ((TecnicoRow)(this.Rows.Find(new object[] {
                            Id})));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public override global::System.Data.DataTable Clone() {
                TecnicoDataTable cln = ((TecnicoDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Data.DataTable CreateInstance() {
                return new TecnicoDataTable();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal void InitVars() {
                this.columnId = base.Columns["Id"];
                this.columnLegajo = base.Columns["Legajo"];
                this.columnMail = base.Columns["Mail"];
                this.columnNombreApellido = base.Columns["NombreApellido"];
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            private void InitClass() {
                this.columnId = new global::System.Data.DataColumn("Id", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnId);
                this.columnLegajo = new global::System.Data.DataColumn("Legajo", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnLegajo);
                this.columnMail = new global::System.Data.DataColumn("Mail", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnMail);
                this.columnNombreApellido = new global::System.Data.DataColumn("NombreApellido", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnNombreApellido);
                this.Constraints.Add(new global::System.Data.UniqueConstraint("Constraint1", new global::System.Data.DataColumn[] {
                                this.columnId}, true));
                this.columnId.AutoIncrement = true;
                this.columnId.AutoIncrementSeed = -1;
                this.columnId.AutoIncrementStep = -1;
                this.columnId.AllowDBNull = false;
                this.columnId.ReadOnly = true;
                this.columnId.Unique = true;
                this.columnLegajo.AllowDBNull = false;
                this.columnLegajo.MaxLength = 50;
                this.columnMail.AllowDBNull = false;
                this.columnMail.MaxLength = 50;
                this.columnNombreApellido.AllowDBNull = false;
                this.columnNombreApellido.MaxLength = 50;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TecnicoRow NewTecnicoRow() {
                return ((TecnicoRow)(this.NewRow()));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new TecnicoRow(builder);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Type GetRowType() {
                return typeof(TecnicoRow);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TecnicoRowChanged != null)) {
                    this.TecnicoRowChanged(this, new TecnicoRowChangeEvent(((TecnicoRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TecnicoRowChanging != null)) {
                    this.TecnicoRowChanging(this, new TecnicoRowChangeEvent(((TecnicoRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TecnicoRowDeleted != null)) {
                    this.TecnicoRowDeleted(this, new TecnicoRowChangeEvent(((TecnicoRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TecnicoRowDeleting != null)) {
                    this.TecnicoRowDeleting(this, new TecnicoRowChangeEvent(((TecnicoRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public void RemoveTecnicoRow(TecnicoRow row) {
                this.Rows.Remove(row);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                TicketsProDataSet ds = new TicketsProDataSet();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "TecnicoDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class TicketDataTable : global::System.Data.TypedTableBase<TicketRow> {
            private global::System.Data.DataColumn columnTicket;
            private global::System.Data.DataColumn columnTitulo;
            private global::System.Data.DataColumn columnDescripcion;
            private global::System.Data.DataColumn columnFechaApertura;
            private global::System.Data.DataColumn columnCategoria;
            private global::System.Data.DataColumn columnEstado;
            private global::System.Data.DataColumn columnUbicacion;
            private global::System.Data.DataColumn columnTecnicoAsignadoId;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TicketDataTable() {
                this.TableName = "Ticket";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal TicketDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected TicketDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn TicketColumn {
                get {
                    return this.columnTicket;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn TituloColumn {
                get {
                    return this.columnTitulo;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn DescripcionColumn {
                get {
                    return this.columnDescripcion;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn FechaAperturaColumn {
                get {
                    return this.columnFechaApertura;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn CategoriaColumn {
                get {
                    return this.columnCategoria;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn EstadoColumn {
                get {
                    return this.columnEstado;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn UbicacionColumn {
                get {
                    return this.columnUbicacion;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataColumn TecnicoAsignadoIdColumn {
                get {
                    return this.columnTecnicoAsignadoId;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TicketRow this[int index] {
                get {
                    return ((TicketRow)(this.Rows[index]));
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event TicketRowChangeEventHandler TicketRowChanging;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event TicketRowChangeEventHandler TicketRowChanged;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event TicketRowChangeEventHandler TicketRowDeleting;
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public event TicketRowChangeEventHandler TicketRowDeleted;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public void AddTicketRow(TicketRow row) {
                this.Rows.Add(row);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TicketRow AddTicketRow(string Titulo, string Descripcion, System.DateTime FechaApertura, int Categoria, int Estado, int Ubicacion, int TecnicoAsignadoId) {
                TicketRow rowTicketRow = ((TicketRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        null,
                        Titulo,
                        Descripcion,
                        FechaApertura,
                        Categoria,
                        Estado,
                        Ubicacion,
                        TecnicoAsignadoId};
                rowTicketRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowTicketRow);
                return rowTicketRow;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TicketRow FindByTicket(int Ticket) {
                return ((TicketRow)(this.Rows.Find(new object[] {
                            Ticket})));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public override global::System.Data.DataTable Clone() {
                TicketDataTable cln = ((TicketDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Data.DataTable CreateInstance() {
                return new TicketDataTable();
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal void InitVars() {
                this.columnTicket = base.Columns["Ticket"];
                this.columnTitulo = base.Columns["Titulo"];
                this.columnDescripcion = base.Columns["Descripcion"];
                this.columnFechaApertura = base.Columns["FechaApertura"];
                this.columnCategoria = base.Columns["Categoria"];
                this.columnEstado = base.Columns["Estado"];
                this.columnUbicacion = base.Columns["Ubicacion"];
                this.columnTecnicoAsignadoId = base.Columns["TecnicoAsignadoId"];
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            private void InitClass() {
                this.columnTicket = new global::System.Data.DataColumn("Ticket", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnTicket);
                this.columnTitulo = new global::System.Data.DataColumn("Titulo", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnTitulo);
                this.columnDescripcion = new global::System.Data.DataColumn("Descripcion", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDescripcion);
                this.columnFechaApertura = new global::System.Data.DataColumn("FechaApertura", typeof(global::System.DateTime), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnFechaApertura);
                this.columnCategoria = new global::System.Data.DataColumn("Categoria", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCategoria);
                this.columnEstado = new global::System.Data.DataColumn("Estado", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnEstado);
                this.columnUbicacion = new global::System.Data.DataColumn("Ubicacion", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnUbicacion);
                this.columnTecnicoAsignadoId = new global::System.Data.DataColumn("TecnicoAsignadoId", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnTecnicoAsignadoId);
                this.Constraints.Add(new global::System.Data.UniqueConstraint("Constraint1", new global::System.Data.DataColumn[] {
                                this.columnTicket}, true));
                this.columnTicket.AutoIncrement = true;
                this.columnTicket.AutoIncrementSeed = -1;
                this.columnTicket.AutoIncrementStep = -1;
                this.columnTicket.AllowDBNull = false;
                this.columnTicket.ReadOnly = true;
                this.columnTicket.Unique = true;
                this.columnTitulo.AllowDBNull = false;
                this.columnTitulo.MaxLength = 50;
                this.columnDescripcion.AllowDBNull = false;
                this.columnDescripcion.MaxLength = 50;
                this.columnFechaApertura.AllowDBNull = false;
                this.columnCategoria.AllowDBNull = false;
                this.columnEstado.AllowDBNull = false;
                this.columnUbicacion.AllowDBNull = false;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TicketRow NewTicketRow() {
                return ((TicketRow)(this.NewRow()));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new TicketRow(builder);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override global::System.Type GetRowType() {
                return typeof(TicketRow);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.TicketRowChanged != null)) {
                    this.TicketRowChanged(this, new TicketRowChangeEvent(((TicketRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.TicketRowChanging != null)) {
                    this.TicketRowChanging(this, new TicketRowChangeEvent(((TicketRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.TicketRowDeleted != null)) {
                    this.TicketRowDeleted(this, new TicketRowChangeEvent(((TicketRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.TicketRowDeleting != null)) {
                    this.TicketRowDeleting(this, new TicketRowChangeEvent(((TicketRow)(e.Row)), e.Action));
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public void RemoveTicketRow(TicketRow row) {
                this.Rows.Remove(row);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                TicketsProDataSet ds = new TicketsProDataSet();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "TicketDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        public partial class EquipoInformaticoRow : global::System.Data.DataRow {
            private EquipoInformaticoDataTable tableEquipoInformatico;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal EquipoInformaticoRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableEquipoInformatico = ((EquipoInformaticoDataTable)(this.Table));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int Id {
                get {
                    return ((int)(this[this.tableEquipoInformatico.IdColumn]));
                }
                set {
                    this[this.tableEquipoInformatico.IdColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public string ModeloEquipo {
                get {
                    return ((string)(this[this.tableEquipoInformatico.ModeloEquipoColumn]));
                }
                set {
                    this[this.tableEquipoInformatico.ModeloEquipoColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public string NroInventario {
                get {
                    return ((string)(this[this.tableEquipoInformatico.NroInventarioColumn]));
                }
                set {
                    this[this.tableEquipoInformatico.NroInventarioColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public string Procesador {
                get {
                    return ((string)(this[this.tableEquipoInformatico.ProcesadorColumn]));
                }
                set {
                    this[this.tableEquipoInformatico.ProcesadorColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int RAM {
                get {
                    return ((int)(this[this.tableEquipoInformatico.RAMColumn]));
                }
                set {
                    this[this.tableEquipoInformatico.RAMColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int ROM {
                get {
                    return ((int)(this[this.tableEquipoInformatico.ROMColumn]));
                }
                set {
                    this[this.tableEquipoInformatico.ROMColumn] = value;
                }
            }
        }
        public partial class SolucionTicketRow : global::System.Data.DataRow {
            private SolucionTicketDataTable tableSolucionTicket;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal SolucionTicketRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableSolucionTicket = ((SolucionTicketDataTable)(this.Table));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int Id {
                get {
                    return ((int)(this[this.tableSolucionTicket.IdColumn]));
                }
                set {
                    this[this.tableSolucionTicket.IdColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public string DescripcionSolucion {
                get {
                    return ((string)(this[this.tableSolucionTicket.DescripcionSolucionColumn]));
                }
                set {
                    this[this.tableSolucionTicket.DescripcionSolucionColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public System.DateTime FechaCierre {
                get {
                    return ((global::System.DateTime)(this[this.tableSolucionTicket.FechaCierreColumn]));
                }
                set {
                    this[this.tableSolucionTicket.FechaCierreColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int TicketId {
                get {
                    return ((int)(this[this.tableSolucionTicket.TicketIdColumn]));
                }
                set {
                    this[this.tableSolucionTicket.TicketIdColumn] = value;
                }
            }
        }
        public partial class TecnicoRow : global::System.Data.DataRow {
            private TecnicoDataTable tableTecnico;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal TecnicoRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableTecnico = ((TecnicoDataTable)(this.Table));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int Id {
                get {
                    return ((int)(this[this.tableTecnico.IdColumn]));
                }
                set {
                    this[this.tableTecnico.IdColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public string Legajo {
                get {
                    return ((string)(this[this.tableTecnico.LegajoColumn]));
                }
                set {
                    this[this.tableTecnico.LegajoColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public string Mail {
                get {
                    return ((string)(this[this.tableTecnico.MailColumn]));
                }
                set {
                    this[this.tableTecnico.MailColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public string NombreApellido {
                get {
                    return ((string)(this[this.tableTecnico.NombreApellidoColumn]));
                }
                set {
                    this[this.tableTecnico.NombreApellidoColumn] = value;
                }
            }
        }
        public partial class TicketRow : global::System.Data.DataRow {
            private TicketDataTable tableTicket;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal TicketRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableTicket = ((TicketDataTable)(this.Table));
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int Ticket {
                get {
                    return ((int)(this[this.tableTicket.TicketColumn]));
                }
                set {
                    this[this.tableTicket.TicketColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public string Titulo {
                get {
                    return ((string)(this[this.tableTicket.TituloColumn]));
                }
                set {
                    this[this.tableTicket.TituloColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public string Descripcion {
                get {
                    return ((string)(this[this.tableTicket.DescripcionColumn]));
                }
                set {
                    this[this.tableTicket.DescripcionColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public System.DateTime FechaApertura {
                get {
                    return ((global::System.DateTime)(this[this.tableTicket.FechaAperturaColumn]));
                }
                set {
                    this[this.tableTicket.FechaAperturaColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int Categoria {
                get {
                    return ((int)(this[this.tableTicket.CategoriaColumn]));
                }
                set {
                    this[this.tableTicket.CategoriaColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int Estado {
                get {
                    return ((int)(this[this.tableTicket.EstadoColumn]));
                }
                set {
                    this[this.tableTicket.EstadoColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int Ubicacion {
                get {
                    return ((int)(this[this.tableTicket.UbicacionColumn]));
                }
                set {
                    this[this.tableTicket.UbicacionColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int TecnicoAsignadoId {
                get {
                    try {
                        return ((int)(this[this.tableTicket.TecnicoAsignadoIdColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("El valor de la columna \'TecnicoAsignadoId\' de la tabla \'Ticket\' es DBNull.", e);
                    }
                }
                set {
                    this[this.tableTicket.TecnicoAsignadoIdColumn] = value;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public bool IsTecnicoAsignadoIdNull() {
                return this.IsNull(this.tableTicket.TecnicoAsignadoIdColumn);
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public void SetTecnicoAsignadoIdNull() {
                this[this.tableTicket.TecnicoAsignadoIdColumn] = global::System.Convert.DBNull;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public class EquipoInformaticoRowChangeEvent : global::System.EventArgs {
            private EquipoInformaticoRow eventRow;
            private global::System.Data.DataRowAction eventAction;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public EquipoInformaticoRowChangeEvent(EquipoInformaticoRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public EquipoInformaticoRow Row {
                get {
                    return this.eventRow;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public class SolucionTicketRowChangeEvent : global::System.EventArgs {
            private SolucionTicketRow eventRow;
            private global::System.Data.DataRowAction eventAction;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public SolucionTicketRowChangeEvent(SolucionTicketRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public SolucionTicketRow Row {
                get {
                    return this.eventRow;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public class TecnicoRowChangeEvent : global::System.EventArgs {
            private TecnicoRow eventRow;
            private global::System.Data.DataRowAction eventAction;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TecnicoRowChangeEvent(TecnicoRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TecnicoRow Row {
                get {
                    return this.eventRow;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public class TicketRowChangeEvent : global::System.EventArgs {
            private TicketRow eventRow;
            private global::System.Data.DataRowAction eventAction;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TicketRowChangeEvent(TicketRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public TicketRow Row {
                get {
                    return this.eventRow;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
namespace TicketPro.TicketsProDataSetTableAdapters {
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class EquipoInformaticoTableAdapter : global::System.ComponentModel.Component {
        private global::System.Data.SqlClient.SqlDataAdapter _adapter;
        private global::System.Data.SqlClient.SqlConnection _connection;
        private global::System.Data.SqlClient.SqlTransaction _transaction;
        private global::System.Data.SqlClient.SqlCommand[] _commandCollection;
        private bool _clearBeforeFill;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public EquipoInformaticoTableAdapter() {
            this.ClearBeforeFill = true;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected internal global::System.Data.SqlClient.SqlDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal global::System.Data.SqlClient.SqlConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.SqlClient.SqlCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal global::System.Data.SqlClient.SqlTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected global::System.Data.SqlClient.SqlCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitAdapter() {
            this._adapter = new global::System.Data.SqlClient.SqlDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "EquipoInformatico";
            tableMapping.ColumnMappings.Add("Id", "Id");
            tableMapping.ColumnMappings.Add("ModeloEquipo", "ModeloEquipo");
            tableMapping.ColumnMappings.Add("NroInventario", "NroInventario");
            tableMapping.ColumnMappings.Add("Procesador", "Procesador");
            tableMapping.ColumnMappings.Add("RAM", "RAM");
            tableMapping.ColumnMappings.Add("ROM", "ROM");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = @"DELETE FROM [dbo].[EquipoInformatico] WHERE (([Id] = @Original_Id) AND ([ModeloEquipo] = @Original_ModeloEquipo) AND ([NroInventario] = @Original_NroInventario) AND ([Procesador] = @Original_Procesador) AND ([RAM] = @Original_RAM) AND ([ROM] = @Original_ROM))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Id", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Id", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ModeloEquipo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModeloEquipo", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_NroInventario", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NroInventario", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Procesador", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Procesador", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_RAM", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RAM", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ROM", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ROM", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
            this._adapter.InsertCommand.CommandText = @"INSERT INTO [dbo].[EquipoInformatico] ([ModeloEquipo], [NroInventario], [Procesador], [RAM], [ROM]) VALUES (@ModeloEquipo, @NroInventario, @Procesador, @RAM, @ROM);
SELECT Id, ModeloEquipo, NroInventario, Procesador, RAM, ROM FROM EquipoInformatico WHERE (Id = SCOPE_IDENTITY())";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModeloEquipo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModeloEquipo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NroInventario", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NroInventario", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Procesador", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Procesador", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RAM", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RAM", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ROM", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ROM", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = @"UPDATE [dbo].[EquipoInformatico] SET [ModeloEquipo] = @ModeloEquipo, [NroInventario] = @NroInventario, [Procesador] = @Procesador, [RAM] = @RAM, [ROM] = @ROM WHERE (([Id] = @Original_Id) AND ([ModeloEquipo] = @Original_ModeloEquipo) AND ([NroInventario] = @Original_NroInventario) AND ([Procesador] = @Original_Procesador) AND ([RAM] = @Original_RAM) AND ([ROM] = @Original_ROM));
SELECT Id, ModeloEquipo, NroInventario, Procesador, RAM, ROM FROM EquipoInformatico WHERE (Id = @Id)";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModeloEquipo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModeloEquipo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NroInventario", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NroInventario", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Procesador", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Procesador", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RAM", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RAM", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ROM", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ROM", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Id", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Id", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ModeloEquipo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModeloEquipo", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_NroInventario", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NroInventario", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Procesador", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Procesador", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_RAM", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RAM", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_ROM", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ROM", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Id", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "Id", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitConnection() {
            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = global::TicketPro.Properties.Settings.Default.TicketsProConnectionString;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.SqlClient.SqlCommand[1];
            this._commandCollection[0] = new global::System.Data.SqlClient.SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT Id, ModeloEquipo, NroInventario, Procesador, RAM, ROM FROM dbo.EquipoInfor" +
                "matico";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(TicketsProDataSet.EquipoInformaticoDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual TicketsProDataSet.EquipoInformaticoDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            TicketsProDataSet.EquipoInformaticoDataTable dataTable = new TicketsProDataSet.EquipoInformaticoDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(TicketsProDataSet.EquipoInformaticoDataTable dataTable) {
            return this.Adapter.Update(dataTable);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(TicketsProDataSet dataSet) {
            return this.Adapter.Update(dataSet, "EquipoInformatico");
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow) {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows) {
            return this.Adapter.Update(dataRows);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_Id, string Original_ModeloEquipo, string Original_NroInventario, string Original_Procesador, int Original_RAM, int Original_ROM) {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((int)(Original_Id));
            if ((Original_ModeloEquipo == null)) {
                throw new global::System.ArgumentNullException("Original_ModeloEquipo");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((string)(Original_ModeloEquipo));
            }
            if ((Original_NroInventario == null)) {
                throw new global::System.ArgumentNullException("Original_NroInventario");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((string)(Original_NroInventario));
            }
            if ((Original_Procesador == null)) {
                throw new global::System.ArgumentNullException("Original_Procesador");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[3].Value = ((string)(Original_Procesador));
            }
            this.Adapter.DeleteCommand.Parameters[4].Value = ((int)(Original_RAM));
            this.Adapter.DeleteCommand.Parameters[5].Value = ((int)(Original_ROM));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(string ModeloEquipo, string NroInventario, string Procesador, int RAM, int ROM) {
            if ((ModeloEquipo == null)) {
                throw new global::System.ArgumentNullException("ModeloEquipo");
            }
            else {
                this.Adapter.InsertCommand.Parameters[0].Value = ((string)(ModeloEquipo));
            }
            if ((NroInventario == null)) {
                throw new global::System.ArgumentNullException("NroInventario");
            }
            else {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(NroInventario));
            }
            if ((Procesador == null)) {
                throw new global::System.ArgumentNullException("Procesador");
            }
            else {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(Procesador));
            }
            this.Adapter.InsertCommand.Parameters[3].Value = ((int)(RAM));
            this.Adapter.InsertCommand.Parameters[4].Value = ((int)(ROM));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(string ModeloEquipo, string NroInventario, string Procesador, int RAM, int ROM, int Original_Id, string Original_ModeloEquipo, string Original_NroInventario, string Original_Procesador, int Original_RAM, int Original_ROM, int Id) {
            if ((ModeloEquipo == null)) {
                throw new global::System.ArgumentNullException("ModeloEquipo");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[0].Value = ((string)(ModeloEquipo));
            }
            if ((NroInventario == null)) {
                throw new global::System.ArgumentNullException("NroInventario");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(NroInventario));
            }
            if ((Procesador == null)) {
                throw new global::System.ArgumentNullException("Procesador");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(Procesador));
            }
            this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(RAM));
            this.Adapter.UpdateCommand.Parameters[4].Value = ((int)(ROM));
            this.Adapter.UpdateCommand.Parameters[5].Value = ((int)(Original_Id));
            if ((Original_ModeloEquipo == null)) {
                throw new global::System.ArgumentNullException("Original_ModeloEquipo");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[6].Value = ((string)(Original_ModeloEquipo));
            }
            if ((Original_NroInventario == null)) {
                throw new global::System.ArgumentNullException("Original_NroInventario");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[7].Value = ((string)(Original_NroInventario));
            }
            if ((Original_Procesador == null)) {
                throw new global::System.ArgumentNullException("Original_Procesador");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((string)(Original_Procesador));
            }
            this.Adapter.UpdateCommand.Parameters[9].Value = ((int)(Original_RAM));
            this.Adapter.UpdateCommand.Parameters[10].Value = ((int)(Original_ROM));
            this.Adapter.UpdateCommand.Parameters[11].Value = ((int)(Id));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(string ModeloEquipo, string NroInventario, string Procesador, int RAM, int ROM, int Original_Id, string Original_ModeloEquipo, string Original_NroInventario, string Original_Procesador, int Original_RAM, int Original_ROM) {
            return this.Update(ModeloEquipo, NroInventario, Procesador, RAM, ROM, Original_Id, Original_ModeloEquipo, Original_NroInventario, Original_Procesador, Original_RAM, Original_ROM, Original_Id);
        }
    }
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class SolucionTicketTableAdapter : global::System.ComponentModel.Component {
        private global::System.Data.SqlClient.SqlDataAdapter _adapter;
        private global::System.Data.SqlClient.SqlConnection _connection;
        private global::System.Data.SqlClient.SqlTransaction _transaction;
        private global::System.Data.SqlClient.SqlCommand[] _commandCollection;
        private bool _clearBeforeFill;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public SolucionTicketTableAdapter() {
            this.ClearBeforeFill = true;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected internal global::System.Data.SqlClient.SqlDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal global::System.Data.SqlClient.SqlConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.SqlClient.SqlCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal global::System.Data.SqlClient.SqlTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected global::System.Data.SqlClient.SqlCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitAdapter() {
            this._adapter = new global::System.Data.SqlClient.SqlDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "SolucionTicket";
            tableMapping.ColumnMappings.Add("Id", "Id");
            tableMapping.ColumnMappings.Add("DescripcionSolucion", "DescripcionSolucion");
            tableMapping.ColumnMappings.Add("FechaCierre", "FechaCierre");
            tableMapping.ColumnMappings.Add("TicketId", "TicketId");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = "DELETE FROM [dbo].[SolucionTicket] WHERE (([Id] = @Original_Id) AND ([Descripcion" +
                "Solucion] = @Original_DescripcionSolucion) AND ([FechaCierre] = @Original_FechaC" +
                "ierre) AND ([TicketId] = @Original_TicketId))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Id", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Id", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DescripcionSolucion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DescripcionSolucion", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_FechaCierre", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FechaCierre", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TicketId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TicketId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
            this._adapter.InsertCommand.CommandText = "INSERT INTO [dbo].[SolucionTicket] ([DescripcionSolucion], [FechaCierre], [Ticket" +
                "Id]) VALUES (@DescripcionSolucion, @FechaCierre, @TicketId);\r\nSELECT Id, Descrip" +
                "cionSolucion, FechaCierre, TicketId FROM SolucionTicket WHERE (Id = SCOPE_IDENTI" +
                "TY())";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DescripcionSolucion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DescripcionSolucion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FechaCierre", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FechaCierre", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TicketId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TicketId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = @"UPDATE [dbo].[SolucionTicket] SET [DescripcionSolucion] = @DescripcionSolucion, [FechaCierre] = @FechaCierre, [TicketId] = @TicketId WHERE (([Id] = @Original_Id) AND ([DescripcionSolucion] = @Original_DescripcionSolucion) AND ([FechaCierre] = @Original_FechaCierre) AND ([TicketId] = @Original_TicketId));
SELECT Id, DescripcionSolucion, FechaCierre, TicketId FROM SolucionTicket WHERE (Id = @Id)";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DescripcionSolucion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DescripcionSolucion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FechaCierre", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FechaCierre", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TicketId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TicketId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Id", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Id", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_DescripcionSolucion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DescripcionSolucion", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_FechaCierre", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FechaCierre", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TicketId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TicketId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Id", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "Id", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitConnection() {
            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = global::TicketPro.Properties.Settings.Default.TicketsProConnectionString;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.SqlClient.SqlCommand[1];
            this._commandCollection[0] = new global::System.Data.SqlClient.SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT Id, DescripcionSolucion, FechaCierre, TicketId FROM dbo.SolucionTicket";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(TicketsProDataSet.SolucionTicketDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual TicketsProDataSet.SolucionTicketDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            TicketsProDataSet.SolucionTicketDataTable dataTable = new TicketsProDataSet.SolucionTicketDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(TicketsProDataSet.SolucionTicketDataTable dataTable) {
            return this.Adapter.Update(dataTable);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(TicketsProDataSet dataSet) {
            return this.Adapter.Update(dataSet, "SolucionTicket");
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow) {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows) {
            return this.Adapter.Update(dataRows);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_Id, string Original_DescripcionSolucion, System.DateTime Original_FechaCierre, int Original_TicketId) {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((int)(Original_Id));
            if ((Original_DescripcionSolucion == null)) {
                throw new global::System.ArgumentNullException("Original_DescripcionSolucion");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((string)(Original_DescripcionSolucion));
            }
            this.Adapter.DeleteCommand.Parameters[2].Value = ((System.DateTime)(Original_FechaCierre));
            this.Adapter.DeleteCommand.Parameters[3].Value = ((int)(Original_TicketId));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(string DescripcionSolucion, System.DateTime FechaCierre, int TicketId) {
            if ((DescripcionSolucion == null)) {
                throw new global::System.ArgumentNullException("DescripcionSolucion");
            }
            else {
                this.Adapter.InsertCommand.Parameters[0].Value = ((string)(DescripcionSolucion));
            }
            this.Adapter.InsertCommand.Parameters[1].Value = ((System.DateTime)(FechaCierre));
            this.Adapter.InsertCommand.Parameters[2].Value = ((int)(TicketId));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(string DescripcionSolucion, System.DateTime FechaCierre, int TicketId, int Original_Id, string Original_DescripcionSolucion, System.DateTime Original_FechaCierre, int Original_TicketId, int Id) {
            if ((DescripcionSolucion == null)) {
                throw new global::System.ArgumentNullException("DescripcionSolucion");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[0].Value = ((string)(DescripcionSolucion));
            }
            this.Adapter.UpdateCommand.Parameters[1].Value = ((System.DateTime)(FechaCierre));
            this.Adapter.UpdateCommand.Parameters[2].Value = ((int)(TicketId));
            this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(Original_Id));
            if ((Original_DescripcionSolucion == null)) {
                throw new global::System.ArgumentNullException("Original_DescripcionSolucion");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(Original_DescripcionSolucion));
            }
            this.Adapter.UpdateCommand.Parameters[5].Value = ((System.DateTime)(Original_FechaCierre));
            this.Adapter.UpdateCommand.Parameters[6].Value = ((int)(Original_TicketId));
            this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Id));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(string DescripcionSolucion, System.DateTime FechaCierre, int TicketId, int Original_Id, string Original_DescripcionSolucion, System.DateTime Original_FechaCierre, int Original_TicketId) {
            return this.Update(DescripcionSolucion, FechaCierre, TicketId, Original_Id, Original_DescripcionSolucion, Original_FechaCierre, Original_TicketId, Original_Id);
        }
    }
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class TecnicoTableAdapter : global::System.ComponentModel.Component {
        private global::System.Data.SqlClient.SqlDataAdapter _adapter;
        private global::System.Data.SqlClient.SqlConnection _connection;
        private global::System.Data.SqlClient.SqlTransaction _transaction;
        private global::System.Data.SqlClient.SqlCommand[] _commandCollection;
        private bool _clearBeforeFill;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public TecnicoTableAdapter() {
            this.ClearBeforeFill = true;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected internal global::System.Data.SqlClient.SqlDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal global::System.Data.SqlClient.SqlConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.SqlClient.SqlCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal global::System.Data.SqlClient.SqlTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected global::System.Data.SqlClient.SqlCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitAdapter() {
            this._adapter = new global::System.Data.SqlClient.SqlDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Tecnico";
            tableMapping.ColumnMappings.Add("Id", "Id");
            tableMapping.ColumnMappings.Add("Legajo", "Legajo");
            tableMapping.ColumnMappings.Add("Mail", "Mail");
            tableMapping.ColumnMappings.Add("NombreApellido", "NombreApellido");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = "DELETE FROM [dbo].[Tecnico] WHERE (([Id] = @Original_Id) AND ([Legajo] = @Origina" +
                "l_Legajo) AND ([Mail] = @Original_Mail) AND ([NombreApellido] = @Original_Nombre" +
                "Apellido))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Id", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Id", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Legajo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Legajo", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Mail", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Mail", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_NombreApellido", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NombreApellido", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
            this._adapter.InsertCommand.CommandText = "INSERT INTO [dbo].[Tecnico] ([Legajo], [Mail], [NombreApellido]) VALUES (@Legajo," +
                " @Mail, @NombreApellido);\r\nSELECT Id, Legajo, Mail, NombreApellido FROM Tecnico " +
                "WHERE (Id = SCOPE_IDENTITY())";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Legajo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Legajo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Mail", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Mail", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NombreApellido", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NombreApellido", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = @"UPDATE [dbo].[Tecnico] SET [Legajo] = @Legajo, [Mail] = @Mail, [NombreApellido] = @NombreApellido WHERE (([Id] = @Original_Id) AND ([Legajo] = @Original_Legajo) AND ([Mail] = @Original_Mail) AND ([NombreApellido] = @Original_NombreApellido));
SELECT Id, Legajo, Mail, NombreApellido FROM Tecnico WHERE (Id = @Id)";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Legajo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Legajo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Mail", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Mail", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NombreApellido", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NombreApellido", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Id", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Id", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Legajo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Legajo", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Mail", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Mail", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_NombreApellido", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NombreApellido", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Id", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "Id", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitConnection() {
            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = global::TicketPro.Properties.Settings.Default.TicketsProConnectionString;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.SqlClient.SqlCommand[1];
            this._commandCollection[0] = new global::System.Data.SqlClient.SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT Id, Legajo, Mail, NombreApellido FROM dbo.Tecnico";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(TicketsProDataSet.TecnicoDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual TicketsProDataSet.TecnicoDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            TicketsProDataSet.TecnicoDataTable dataTable = new TicketsProDataSet.TecnicoDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(TicketsProDataSet.TecnicoDataTable dataTable) {
            return this.Adapter.Update(dataTable);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(TicketsProDataSet dataSet) {
            return this.Adapter.Update(dataSet, "Tecnico");
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow) {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows) {
            return this.Adapter.Update(dataRows);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_Id, string Original_Legajo, string Original_Mail, string Original_NombreApellido) {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((int)(Original_Id));
            if ((Original_Legajo == null)) {
                throw new global::System.ArgumentNullException("Original_Legajo");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((string)(Original_Legajo));
            }
            if ((Original_Mail == null)) {
                throw new global::System.ArgumentNullException("Original_Mail");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((string)(Original_Mail));
            }
            if ((Original_NombreApellido == null)) {
                throw new global::System.ArgumentNullException("Original_NombreApellido");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[3].Value = ((string)(Original_NombreApellido));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(string Legajo, string Mail, string NombreApellido) {
            if ((Legajo == null)) {
                throw new global::System.ArgumentNullException("Legajo");
            }
            else {
                this.Adapter.InsertCommand.Parameters[0].Value = ((string)(Legajo));
            }
            if ((Mail == null)) {
                throw new global::System.ArgumentNullException("Mail");
            }
            else {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(Mail));
            }
            if ((NombreApellido == null)) {
                throw new global::System.ArgumentNullException("NombreApellido");
            }
            else {
                this.Adapter.InsertCommand.Parameters[2].Value = ((string)(NombreApellido));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(string Legajo, string Mail, string NombreApellido, int Original_Id, string Original_Legajo, string Original_Mail, string Original_NombreApellido, int Id) {
            if ((Legajo == null)) {
                throw new global::System.ArgumentNullException("Legajo");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[0].Value = ((string)(Legajo));
            }
            if ((Mail == null)) {
                throw new global::System.ArgumentNullException("Mail");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(Mail));
            }
            if ((NombreApellido == null)) {
                throw new global::System.ArgumentNullException("NombreApellido");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((string)(NombreApellido));
            }
            this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(Original_Id));
            if ((Original_Legajo == null)) {
                throw new global::System.ArgumentNullException("Original_Legajo");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((string)(Original_Legajo));
            }
            if ((Original_Mail == null)) {
                throw new global::System.ArgumentNullException("Original_Mail");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[5].Value = ((string)(Original_Mail));
            }
            if ((Original_NombreApellido == null)) {
                throw new global::System.ArgumentNullException("Original_NombreApellido");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[6].Value = ((string)(Original_NombreApellido));
            }
            this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Id));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(string Legajo, string Mail, string NombreApellido, int Original_Id, string Original_Legajo, string Original_Mail, string Original_NombreApellido) {
            return this.Update(Legajo, Mail, NombreApellido, Original_Id, Original_Legajo, Original_Mail, Original_NombreApellido, Original_Id);
        }
    }
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class TicketTableAdapter : global::System.ComponentModel.Component {
        private global::System.Data.SqlClient.SqlDataAdapter _adapter;
        private global::System.Data.SqlClient.SqlConnection _connection;
        private global::System.Data.SqlClient.SqlTransaction _transaction;
        private global::System.Data.SqlClient.SqlCommand[] _commandCollection;
        private bool _clearBeforeFill;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public TicketTableAdapter() {
            this.ClearBeforeFill = true;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected internal global::System.Data.SqlClient.SqlDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal global::System.Data.SqlClient.SqlConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.SqlClient.SqlCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        internal global::System.Data.SqlClient.SqlTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected global::System.Data.SqlClient.SqlCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitAdapter() {
            this._adapter = new global::System.Data.SqlClient.SqlDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Ticket";
            tableMapping.ColumnMappings.Add("Ticket", "Ticket");
            tableMapping.ColumnMappings.Add("Titulo", "Titulo");
            tableMapping.ColumnMappings.Add("Descripcion", "Descripcion");
            tableMapping.ColumnMappings.Add("FechaApertura", "FechaApertura");
            tableMapping.ColumnMappings.Add("Categoria", "Categoria");
            tableMapping.ColumnMappings.Add("Estado", "Estado");
            tableMapping.ColumnMappings.Add("Ubicacion", "Ubicacion");
            tableMapping.ColumnMappings.Add("TecnicoAsignadoId", "TecnicoAsignadoId");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = @"DELETE FROM [dbo].[Ticket] WHERE (([Ticket] = @Original_Ticket) AND ([Titulo] = @Original_Titulo) AND ([Descripcion] = @Original_Descripcion) AND ([FechaApertura] = @Original_FechaApertura) AND ([Categoria] = @Original_Categoria) AND ([Estado] = @Original_Estado) AND ([Ubicacion] = @Original_Ubicacion) AND ((@IsNull_TecnicoAsignadoId = 1 AND [TecnicoAsignadoId] IS NULL) OR ([TecnicoAsignadoId] = @Original_TecnicoAsignadoId)))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Ticket", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Ticket", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Titulo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Titulo", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Descripcion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Descripcion", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_FechaApertura", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FechaApertura", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Categoria", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Categoria", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Estado", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Estado", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Ubicacion", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Ubicacion", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_TecnicoAsignadoId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TecnicoAsignadoId", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TecnicoAsignadoId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TecnicoAsignadoId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
            this._adapter.InsertCommand.CommandText = @"INSERT INTO [dbo].[Ticket] ([Titulo], [Descripcion], [FechaApertura], [Categoria], [Estado], [Ubicacion], [TecnicoAsignadoId]) VALUES (@Titulo, @Descripcion, @FechaApertura, @Categoria, @Estado, @Ubicacion, @TecnicoAsignadoId);
SELECT Ticket, Titulo, Descripcion, FechaApertura, Categoria, Estado, Ubicacion, TecnicoAsignadoId FROM Ticket WHERE (Ticket = SCOPE_IDENTITY())";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Titulo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Titulo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Descripcion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Descripcion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FechaApertura", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FechaApertura", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Categoria", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Categoria", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Estado", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Estado", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Ubicacion", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Ubicacion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TecnicoAsignadoId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TecnicoAsignadoId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = @"UPDATE [dbo].[Ticket] SET [Titulo] = @Titulo, [Descripcion] = @Descripcion, [FechaApertura] = @FechaApertura, [Categoria] = @Categoria, [Estado] = @Estado, [Ubicacion] = @Ubicacion, [TecnicoAsignadoId] = @TecnicoAsignadoId WHERE (([Ticket] = @Original_Ticket) AND ([Titulo] = @Original_Titulo) AND ([Descripcion] = @Original_Descripcion) AND ([FechaApertura] = @Original_FechaApertura) AND ([Categoria] = @Original_Categoria) AND ([Estado] = @Original_Estado) AND ([Ubicacion] = @Original_Ubicacion) AND ((@IsNull_TecnicoAsignadoId = 1 AND [TecnicoAsignadoId] IS NULL) OR ([TecnicoAsignadoId] = @Original_TecnicoAsignadoId)));
SELECT Ticket, Titulo, Descripcion, FechaApertura, Categoria, Estado, Ubicacion, TecnicoAsignadoId FROM Ticket WHERE (Ticket = @Ticket)";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Titulo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Titulo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Descripcion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Descripcion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FechaApertura", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FechaApertura", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Categoria", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Categoria", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Estado", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Estado", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Ubicacion", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Ubicacion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TecnicoAsignadoId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TecnicoAsignadoId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Ticket", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Ticket", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Titulo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Titulo", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Descripcion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Descripcion", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_FechaApertura", global::System.Data.SqlDbType.Date, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FechaApertura", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Categoria", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Categoria", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Estado", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Estado", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_Ubicacion", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Ubicacion", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsNull_TecnicoAsignadoId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TecnicoAsignadoId", global::System.Data.DataRowVersion.Original, true, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_TecnicoAsignadoId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TecnicoAsignadoId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Ticket", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "Ticket", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitConnection() {
            this._connection = new global::System.Data.SqlClient.SqlConnection();
            this._connection.ConnectionString = global::TicketPro.Properties.Settings.Default.TicketsProConnectionString;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.SqlClient.SqlCommand[1];
            this._commandCollection[0] = new global::System.Data.SqlClient.SqlCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT Ticket, Titulo, Descripcion, FechaApertura, Categoria, Estado, Ubicacion, " +
                "TecnicoAsignadoId FROM dbo.Ticket";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(TicketsProDataSet.TicketDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual TicketsProDataSet.TicketDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            TicketsProDataSet.TicketDataTable dataTable = new TicketsProDataSet.TicketDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(TicketsProDataSet.TicketDataTable dataTable) {
            return this.Adapter.Update(dataTable);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(TicketsProDataSet dataSet) {
            return this.Adapter.Update(dataSet, "Ticket");
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow) {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows) {
            return this.Adapter.Update(dataRows);
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_Ticket, string Original_Titulo, string Original_Descripcion, System.DateTime Original_FechaApertura, int Original_Categoria, int Original_Estado, int Original_Ubicacion, global::System.Nullable<int> Original_TecnicoAsignadoId) {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((int)(Original_Ticket));
            if ((Original_Titulo == null)) {
                throw new global::System.ArgumentNullException("Original_Titulo");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((string)(Original_Titulo));
            }
            if ((Original_Descripcion == null)) {
                throw new global::System.ArgumentNullException("Original_Descripcion");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[2].Value = ((string)(Original_Descripcion));
            }
            this.Adapter.DeleteCommand.Parameters[3].Value = ((System.DateTime)(Original_FechaApertura));
            this.Adapter.DeleteCommand.Parameters[4].Value = ((int)(Original_Categoria));
            this.Adapter.DeleteCommand.Parameters[5].Value = ((int)(Original_Estado));
            this.Adapter.DeleteCommand.Parameters[6].Value = ((int)(Original_Ubicacion));
            if ((Original_TecnicoAsignadoId.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[7].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[8].Value = ((int)(Original_TecnicoAsignadoId.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[7].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[8].Value = global::System.DBNull.Value;
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(string Titulo, string Descripcion, System.DateTime FechaApertura, int Categoria, int Estado, int Ubicacion, global::System.Nullable<int> TecnicoAsignadoId) {
            if ((Titulo == null)) {
                throw new global::System.ArgumentNullException("Titulo");
            }
            else {
                this.Adapter.InsertCommand.Parameters[0].Value = ((string)(Titulo));
            }
            if ((Descripcion == null)) {
                throw new global::System.ArgumentNullException("Descripcion");
            }
            else {
                this.Adapter.InsertCommand.Parameters[1].Value = ((string)(Descripcion));
            }
            this.Adapter.InsertCommand.Parameters[2].Value = ((System.DateTime)(FechaApertura));
            this.Adapter.InsertCommand.Parameters[3].Value = ((int)(Categoria));
            this.Adapter.InsertCommand.Parameters[4].Value = ((int)(Estado));
            this.Adapter.InsertCommand.Parameters[5].Value = ((int)(Ubicacion));
            if ((TecnicoAsignadoId.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[6].Value = ((int)(TecnicoAsignadoId.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[6].Value = global::System.DBNull.Value;
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(
                    string Titulo, 
                    string Descripcion, 
                    System.DateTime FechaApertura, 
                    int Categoria, 
                    int Estado, 
                    int Ubicacion, 
                    global::System.Nullable<int> TecnicoAsignadoId, 
                    int Original_Ticket, 
                    string Original_Titulo, 
                    string Original_Descripcion, 
                    System.DateTime Original_FechaApertura, 
                    int Original_Categoria, 
                    int Original_Estado, 
                    int Original_Ubicacion, 
                    global::System.Nullable<int> Original_TecnicoAsignadoId, 
                    int Ticket) {
            if ((Titulo == null)) {
                throw new global::System.ArgumentNullException("Titulo");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[0].Value = ((string)(Titulo));
            }
            if ((Descripcion == null)) {
                throw new global::System.ArgumentNullException("Descripcion");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((string)(Descripcion));
            }
            this.Adapter.UpdateCommand.Parameters[2].Value = ((System.DateTime)(FechaApertura));
            this.Adapter.UpdateCommand.Parameters[3].Value = ((int)(Categoria));
            this.Adapter.UpdateCommand.Parameters[4].Value = ((int)(Estado));
            this.Adapter.UpdateCommand.Parameters[5].Value = ((int)(Ubicacion));
            if ((TecnicoAsignadoId.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[6].Value = ((int)(TecnicoAsignadoId.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[6].Value = global::System.DBNull.Value;
            }
            this.Adapter.UpdateCommand.Parameters[7].Value = ((int)(Original_Ticket));
            if ((Original_Titulo == null)) {
                throw new global::System.ArgumentNullException("Original_Titulo");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((string)(Original_Titulo));
            }
            if ((Original_Descripcion == null)) {
                throw new global::System.ArgumentNullException("Original_Descripcion");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[9].Value = ((string)(Original_Descripcion));
            }
            this.Adapter.UpdateCommand.Parameters[10].Value = ((System.DateTime)(Original_FechaApertura));
            this.Adapter.UpdateCommand.Parameters[11].Value = ((int)(Original_Categoria));
            this.Adapter.UpdateCommand.Parameters[12].Value = ((int)(Original_Estado));
            this.Adapter.UpdateCommand.Parameters[13].Value = ((int)(Original_Ubicacion));
            if ((Original_TecnicoAsignadoId.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[14].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[15].Value = ((int)(Original_TecnicoAsignadoId.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[14].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[15].Value = global::System.DBNull.Value;
            }
            this.Adapter.UpdateCommand.Parameters[16].Value = ((int)(Ticket));
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != global::System.Data.ConnectionState.Open)) {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(string Titulo, string Descripcion, System.DateTime FechaApertura, int Categoria, int Estado, int Ubicacion, global::System.Nullable<int> TecnicoAsignadoId, int Original_Ticket, string Original_Titulo, string Original_Descripcion, System.DateTime Original_FechaApertura, int Original_Categoria, int Original_Estado, int Original_Ubicacion, global::System.Nullable<int> Original_TecnicoAsignadoId) {
            return this.Update(Titulo, Descripcion, FechaApertura, Categoria, Estado, Ubicacion, TecnicoAsignadoId, Original_Ticket, Original_Titulo, Original_Descripcion, Original_FechaApertura, Original_Categoria, Original_Estado, Original_Ubicacion, Original_TecnicoAsignadoId, Original_Ticket);
        }
    }
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerDesigner, Microsoft.VSD" +
        "esigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapterManager")]
    public partial class TableAdapterManager : global::System.ComponentModel.Component {
        private UpdateOrderOption _updateOrder;
        private EquipoInformaticoTableAdapter _equipoInformaticoTableAdapter;
        private SolucionTicketTableAdapter _solucionTicketTableAdapter;
        private TecnicoTableAdapter _tecnicoTableAdapter;
        private TicketTableAdapter _ticketTableAdapter;
        private bool _backupDataSetBeforeUpdate;
        private global::System.Data.IDbConnection _connection;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public UpdateOrderOption UpdateOrder {
            get {
                return this._updateOrder;
            }
            set {
                this._updateOrder = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
            "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" +
            "a", "System.Drawing.Design.UITypeEditor")]
        public EquipoInformaticoTableAdapter EquipoInformaticoTableAdapter {
            get {
                return this._equipoInformaticoTableAdapter;
            }
            set {
                this._equipoInformaticoTableAdapter = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
            "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" +
            "a", "System.Drawing.Design.UITypeEditor")]
        public SolucionTicketTableAdapter SolucionTicketTableAdapter {
            get {
                return this._solucionTicketTableAdapter;
            }
            set {
                this._solucionTicketTableAdapter = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
            "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" +
            "a", "System.Drawing.Design.UITypeEditor")]
        public TecnicoTableAdapter TecnicoTableAdapter {
            get {
                return this._tecnicoTableAdapter;
            }
            set {
                this._tecnicoTableAdapter = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
            "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" +
            "a", "System.Drawing.Design.UITypeEditor")]
        public TicketTableAdapter TicketTableAdapter {
            get {
                return this._ticketTableAdapter;
            }
            set {
                this._ticketTableAdapter = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public bool BackupDataSetBeforeUpdate {
            get {
                return this._backupDataSetBeforeUpdate;
            }
            set {
                this._backupDataSetBeforeUpdate = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        public global::System.Data.IDbConnection Connection {
            get {
                if ((this._connection != null)) {
                    return this._connection;
                }
                if (((this._equipoInformaticoTableAdapter != null) 
                            && (this._equipoInformaticoTableAdapter.Connection != null))) {
                    return this._equipoInformaticoTableAdapter.Connection;
                }
                if (((this._solucionTicketTableAdapter != null) 
                            && (this._solucionTicketTableAdapter.Connection != null))) {
                    return this._solucionTicketTableAdapter.Connection;
                }
                if (((this._tecnicoTableAdapter != null) 
                            && (this._tecnicoTableAdapter.Connection != null))) {
                    return this._tecnicoTableAdapter.Connection;
                }
                if (((this._ticketTableAdapter != null) 
                            && (this._ticketTableAdapter.Connection != null))) {
                    return this._ticketTableAdapter.Connection;
                }
                return null;
            }
            set {
                this._connection = value;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        public int TableAdapterInstanceCount {
            get {
                int count = 0;
                if ((this._equipoInformaticoTableAdapter != null)) {
                    count = (count + 1);
                }
                if ((this._solucionTicketTableAdapter != null)) {
                    count = (count + 1);
                }
                if ((this._tecnicoTableAdapter != null)) {
                    count = (count + 1);
                }
                if ((this._ticketTableAdapter != null)) {
                    count = (count + 1);
                }
                return count;
            }
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private int UpdateUpdatedRows(TicketsProDataSet dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows) {
            int result = 0;
            if ((this._equipoInformaticoTableAdapter != null)) {
                global::System.Data.DataRow[] updatedRows = dataSet.EquipoInformatico.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null) 
                            && (0 < updatedRows.Length))) {
                    result = (result + this._equipoInformaticoTableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            if ((this._solucionTicketTableAdapter != null)) {
                global::System.Data.DataRow[] updatedRows = dataSet.SolucionTicket.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null) 
                            && (0 < updatedRows.Length))) {
                    result = (result + this._solucionTicketTableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            if ((this._tecnicoTableAdapter != null)) {
                global::System.Data.DataRow[] updatedRows = dataSet.Tecnico.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null) 
                            && (0 < updatedRows.Length))) {
                    result = (result + this._tecnicoTableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            if ((this._ticketTableAdapter != null)) {
                global::System.Data.DataRow[] updatedRows = dataSet.Ticket.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null) 
                            && (0 < updatedRows.Length))) {
                    result = (result + this._ticketTableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            return result;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private int UpdateInsertedRows(TicketsProDataSet dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows) {
            int result = 0;
            if ((this._equipoInformaticoTableAdapter != null)) {
                global::System.Data.DataRow[] addedRows = dataSet.EquipoInformatico.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null) 
                            && (0 < addedRows.Length))) {
                    result = (result + this._equipoInformaticoTableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            if ((this._solucionTicketTableAdapter != null)) {
                global::System.Data.DataRow[] addedRows = dataSet.SolucionTicket.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null) 
                            && (0 < addedRows.Length))) {
                    result = (result + this._solucionTicketTableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            if ((this._tecnicoTableAdapter != null)) {
                global::System.Data.DataRow[] addedRows = dataSet.Tecnico.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null) 
                            && (0 < addedRows.Length))) {
                    result = (result + this._tecnicoTableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            if ((this._ticketTableAdapter != null)) {
                global::System.Data.DataRow[] addedRows = dataSet.Ticket.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null) 
                            && (0 < addedRows.Length))) {
                    result = (result + this._ticketTableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            return result;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private int UpdateDeletedRows(TicketsProDataSet dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows) {
            int result = 0;
            if ((this._ticketTableAdapter != null)) {
                global::System.Data.DataRow[] deletedRows = dataSet.Ticket.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null) 
                            && (0 < deletedRows.Length))) {
                    result = (result + this._ticketTableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            if ((this._tecnicoTableAdapter != null)) {
                global::System.Data.DataRow[] deletedRows = dataSet.Tecnico.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null) 
                            && (0 < deletedRows.Length))) {
                    result = (result + this._tecnicoTableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            if ((this._solucionTicketTableAdapter != null)) {
                global::System.Data.DataRow[] deletedRows = dataSet.SolucionTicket.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null) 
                            && (0 < deletedRows.Length))) {
                    result = (result + this._solucionTicketTableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            if ((this._equipoInformaticoTableAdapter != null)) {
                global::System.Data.DataRow[] deletedRows = dataSet.EquipoInformatico.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null) 
                            && (0 < deletedRows.Length))) {
                    result = (result + this._equipoInformaticoTableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            return result;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private global::System.Data.DataRow[] GetRealUpdatedRows(global::System.Data.DataRow[] updatedRows, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows) {
            if (((updatedRows == null) 
                        || (updatedRows.Length < 1))) {
                return updatedRows;
            }
            if (((allAddedRows == null) 
                        || (allAddedRows.Count < 1))) {
                return updatedRows;
            }
            global::System.Collections.Generic.List<global::System.Data.DataRow> realUpdatedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            for (int i = 0; (i < updatedRows.Length); i = (i + 1)) {
                global::System.Data.DataRow row = updatedRows[i];
                if ((allAddedRows.Contains(row) == false)) {
                    realUpdatedRows.Add(row);
                }
            }
            return realUpdatedRows.ToArray();
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public virtual int UpdateAll(TicketsProDataSet dataSet) {
            if ((dataSet == null)) {
                throw new global::System.ArgumentNullException("dataSet");
            }
            if ((dataSet.HasChanges() == false)) {
                return 0;
            }
            if (((this._equipoInformaticoTableAdapter != null) 
                        && (this.MatchTableAdapterConnection(this._equipoInformaticoTableAdapter.Connection) == false))) {
                throw new global::System.ArgumentException("Todos los TableAdapters administrados por un TableAdapterManager deben usar la mi" +
                        "sma cadena de conexion.");
            }
            if (((this._solucionTicketTableAdapter != null) 
                        && (this.MatchTableAdapterConnection(this._solucionTicketTableAdapter.Connection) == false))) {
                throw new global::System.ArgumentException("Todos los TableAdapters administrados por un TableAdapterManager deben usar la mi" +
                        "sma cadena de conexion.");
            }
            if (((this._tecnicoTableAdapter != null) 
                        && (this.MatchTableAdapterConnection(this._tecnicoTableAdapter.Connection) == false))) {
                throw new global::System.ArgumentException("Todos los TableAdapters administrados por un TableAdapterManager deben usar la mi" +
                        "sma cadena de conexion.");
            }
            if (((this._ticketTableAdapter != null) 
                        && (this.MatchTableAdapterConnection(this._ticketTableAdapter.Connection) == false))) {
                throw new global::System.ArgumentException("Todos los TableAdapters administrados por un TableAdapterManager deben usar la mi" +
                        "sma cadena de conexion.");
            }
            global::System.Data.IDbConnection workConnection = this.Connection;
            if ((workConnection == null)) {
                throw new global::System.ApplicationException("TableAdapterManager no contiene informacion de conexion. Establezca cada propieda" +
                        "d TableAdapterManager TableAdapter en una instancia TableAdapter valida.");
            }
            bool workConnOpened = false;
            if (((workConnection.State & global::System.Data.ConnectionState.Broken) 
                        == global::System.Data.ConnectionState.Broken)) {
                workConnection.Close();
            }
            if ((workConnection.State == global::System.Data.ConnectionState.Closed)) {
                workConnection.Open();
                workConnOpened = true;
            }
            global::System.Data.IDbTransaction workTransaction = workConnection.BeginTransaction();
            if ((workTransaction == null)) {
                throw new global::System.ApplicationException("La transaccion no puede comenzar. La conexion de datos actual no es compatible co" +
                        "n las transacciones o el estado actual no permite que comience la transaccion.");
            }
            global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            global::System.Collections.Generic.List<global::System.Data.Common.DataAdapter> adaptersWithAcceptChangesDuringUpdate = new global::System.Collections.Generic.List<global::System.Data.Common.DataAdapter>();
            global::System.Collections.Generic.Dictionary<object, global::System.Data.IDbConnection> revertConnections = new global::System.Collections.Generic.Dictionary<object, global::System.Data.IDbConnection>();
            int result = 0;
            global::System.Data.DataSet backupDataSet = null;
            if (this.BackupDataSetBeforeUpdate) {
                backupDataSet = new global::System.Data.DataSet();
                backupDataSet.Merge(dataSet);
            }
            try {
                if ((this._equipoInformaticoTableAdapter != null)) {
                    revertConnections.Add(this._equipoInformaticoTableAdapter, this._equipoInformaticoTableAdapter.Connection);
                    this._equipoInformaticoTableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(workConnection));
                    this._equipoInformaticoTableAdapter.Transaction = ((global::System.Data.SqlClient.SqlTransaction)(workTransaction));
                    if (this._equipoInformaticoTableAdapter.Adapter.AcceptChangesDuringUpdate) {
                        this._equipoInformaticoTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._equipoInformaticoTableAdapter.Adapter);
                    }
                }
                if ((this._solucionTicketTableAdapter != null)) {
                    revertConnections.Add(this._solucionTicketTableAdapter, this._solucionTicketTableAdapter.Connection);
                    this._solucionTicketTableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(workConnection));
                    this._solucionTicketTableAdapter.Transaction = ((global::System.Data.SqlClient.SqlTransaction)(workTransaction));
                    if (this._solucionTicketTableAdapter.Adapter.AcceptChangesDuringUpdate) {
                        this._solucionTicketTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._solucionTicketTableAdapter.Adapter);
                    }
                }
                if ((this._tecnicoTableAdapter != null)) {
                    revertConnections.Add(this._tecnicoTableAdapter, this._tecnicoTableAdapter.Connection);
                    this._tecnicoTableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(workConnection));
                    this._tecnicoTableAdapter.Transaction = ((global::System.Data.SqlClient.SqlTransaction)(workTransaction));
                    if (this._tecnicoTableAdapter.Adapter.AcceptChangesDuringUpdate) {
                        this._tecnicoTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._tecnicoTableAdapter.Adapter);
                    }
                }
                if ((this._ticketTableAdapter != null)) {
                    revertConnections.Add(this._ticketTableAdapter, this._ticketTableAdapter.Connection);
                    this._ticketTableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(workConnection));
                    this._ticketTableAdapter.Transaction = ((global::System.Data.SqlClient.SqlTransaction)(workTransaction));
                    if (this._ticketTableAdapter.Adapter.AcceptChangesDuringUpdate) {
                        this._ticketTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._ticketTableAdapter.Adapter);
                    }
                }
                if ((this.UpdateOrder == UpdateOrderOption.UpdateInsertDelete)) {
                    result = (result + this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows));
                    result = (result + this.UpdateInsertedRows(dataSet, allAddedRows));
                }
                else {
                    result = (result + this.UpdateInsertedRows(dataSet, allAddedRows));
                    result = (result + this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows));
                }
                result = (result + this.UpdateDeletedRows(dataSet, allChangedRows));
                workTransaction.Commit();
                if ((0 < allAddedRows.Count)) {
                    global::System.Data.DataRow[] rows = new System.Data.DataRow[allAddedRows.Count];
                    allAddedRows.CopyTo(rows);
                    for (int i = 0; (i < rows.Length); i = (i + 1)) {
                        global::System.Data.DataRow row = rows[i];
                        row.AcceptChanges();
                    }
                }
                if ((0 < allChangedRows.Count)) {
                    global::System.Data.DataRow[] rows = new System.Data.DataRow[allChangedRows.Count];
                    allChangedRows.CopyTo(rows);
                    for (int i = 0; (i < rows.Length); i = (i + 1)) {
                        global::System.Data.DataRow row = rows[i];
                        row.AcceptChanges();
                    }
                }
            }
            catch (global::System.Exception ex) {
                workTransaction.Rollback();
                if (this.BackupDataSetBeforeUpdate) {
                    global::System.Diagnostics.Debug.Assert((backupDataSet != null));
                    dataSet.Clear();
                    dataSet.Merge(backupDataSet);
                }
                else {
                    if ((0 < allAddedRows.Count)) {
                        global::System.Data.DataRow[] rows = new System.Data.DataRow[allAddedRows.Count];
                        allAddedRows.CopyTo(rows);
                        for (int i = 0; (i < rows.Length); i = (i + 1)) {
                            global::System.Data.DataRow row = rows[i];
                            row.AcceptChanges();
                            row.SetAdded();
                        }
                    }
                }
                throw ex;
            }
            finally {
                if (workConnOpened) {
                    workConnection.Close();
                }
                if ((this._equipoInformaticoTableAdapter != null)) {
                    this._equipoInformaticoTableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(revertConnections[this._equipoInformaticoTableAdapter]));
                    this._equipoInformaticoTableAdapter.Transaction = null;
                }
                if ((this._solucionTicketTableAdapter != null)) {
                    this._solucionTicketTableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(revertConnections[this._solucionTicketTableAdapter]));
                    this._solucionTicketTableAdapter.Transaction = null;
                }
                if ((this._tecnicoTableAdapter != null)) {
                    this._tecnicoTableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(revertConnections[this._tecnicoTableAdapter]));
                    this._tecnicoTableAdapter.Transaction = null;
                }
                if ((this._ticketTableAdapter != null)) {
                    this._ticketTableAdapter.Connection = ((global::System.Data.SqlClient.SqlConnection)(revertConnections[this._ticketTableAdapter]));
                    this._ticketTableAdapter.Transaction = null;
                }
                if ((0 < adaptersWithAcceptChangesDuringUpdate.Count)) {
                    global::System.Data.Common.DataAdapter[] adapters = new System.Data.Common.DataAdapter[adaptersWithAcceptChangesDuringUpdate.Count];
                    adaptersWithAcceptChangesDuringUpdate.CopyTo(adapters);
                    for (int i = 0; (i < adapters.Length); i = (i + 1)) {
                        global::System.Data.Common.DataAdapter adapter = adapters[i];
                        adapter.AcceptChangesDuringUpdate = true;
                    }
                }
            }
            return result;
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected virtual void SortSelfReferenceRows(global::System.Data.DataRow[] rows, global::System.Data.DataRelation relation, bool childFirst) {
            global::System.Array.Sort<global::System.Data.DataRow>(rows, new SelfReferenceComparer(relation, childFirst));
        }
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        protected virtual bool MatchTableAdapterConnection(global::System.Data.IDbConnection inputConnection) {
            if ((this._connection != null)) {
                return true;
            }
            if (((this.Connection == null) 
                        || (inputConnection == null))) {
                return true;
            }
            if (string.Equals(this.Connection.ConnectionString, inputConnection.ConnectionString, global::System.StringComparison.Ordinal)) {
                return true;
            }
            return false;
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        public enum UpdateOrderOption {
            InsertUpdateDelete = 0,
            UpdateInsertDelete = 1,
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
        private class SelfReferenceComparer : object, global::System.Collections.Generic.IComparer<global::System.Data.DataRow> {
            private global::System.Data.DataRelation _relation;
            private int _childFirst;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            internal SelfReferenceComparer(global::System.Data.DataRelation relation, bool childFirst) {
                this._relation = relation;
                if (childFirst) {
                    this._childFirst = -1;
                }
                else {
                    this._childFirst = 1;
                }
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            private global::System.Data.DataRow GetRoot(global::System.Data.DataRow row, out int distance) {
                global::System.Diagnostics.Debug.Assert((row != null));
                global::System.Data.DataRow root = row;
                distance = 0;
                global::System.Collections.Generic.IDictionary<global::System.Data.DataRow, global::System.Data.DataRow> traversedRows = new global::System.Collections.Generic.Dictionary<global::System.Data.DataRow, global::System.Data.DataRow>();
                traversedRows[row] = row;
                global::System.Data.DataRow parent = row.GetParentRow(this._relation, global::System.Data.DataRowVersion.Default);
                for (
                ; ((parent != null) 
                            && (traversedRows.ContainsKey(parent) == false)); 
                ) {
                    distance = (distance + 1);
                    root = parent;
                    traversedRows[parent] = parent;
                    parent = parent.GetParentRow(this._relation, global::System.Data.DataRowVersion.Default);
                }
                if ((distance == 0)) {
                    traversedRows.Clear();
                    traversedRows[row] = row;
                    parent = row.GetParentRow(this._relation, global::System.Data.DataRowVersion.Original);
                    for (
                    ; ((parent != null) 
                                && (traversedRows.ContainsKey(parent) == false)); 
                    ) {
                        distance = (distance + 1);
                        root = parent;
                        traversedRows[parent] = parent;
                        parent = parent.GetParentRow(this._relation, global::System.Data.DataRowVersion.Original);
                    }
                }
                return root;
            }
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "17.0.0.0")]
            public int Compare(global::System.Data.DataRow row1, global::System.Data.DataRow row2) {
                if (object.ReferenceEquals(row1, row2)) {
                    return 0;
                }
                if ((row1 == null)) {
                    return -1;
                }
                if ((row2 == null)) {
                    return 1;
                }
                int distance1 = 0;
                global::System.Data.DataRow root1 = this.GetRoot(row1, out distance1);
                int distance2 = 0;
                global::System.Data.DataRow root2 = this.GetRoot(row2, out distance2);
                if (object.ReferenceEquals(root1, root2)) {
                    return (this._childFirst * distance1.CompareTo(distance2));
                }
                else {
                    global::System.Diagnostics.Debug.Assert(((root1.Table != null) 
                                    && (root2.Table != null)));
                    if ((root1.Table.Rows.IndexOf(root1) < root2.Table.Rows.IndexOf(root2))) {
                        return -1;
                    }
                    else {
                        return 1;
                    }
                }
            }
        }
    }
}
#pragma warning restore 1591
