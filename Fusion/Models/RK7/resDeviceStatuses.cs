﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 
namespace RK7_resDeviceStatuses {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("RK7QueryResult", Namespace="", IsNullable=false)]
    public partial class DevicesRes : RK7QueryResult {
        
        private DevicesDevice[] devicesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Device", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DevicesDevice[] Devices {
            get {
                return this.devicesField;
            }
            set {
                this.devicesField = value;
                this.RaisePropertyChanged("Devices");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DevicesDevice : object, System.ComponentModel.INotifyPropertyChanged {
        
        private DevicesDeviceDeviceState deviceStateField;
        
        private string identField;
        
        private string codeField;
        
        private string nameField;
        
        private string guidField;
        
        private bool loadedField;
        
        private bool loadedFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DevicesDeviceDeviceState DeviceState {
            get {
                return this.deviceStateField;
            }
            set {
                this.deviceStateField = value;
                this.RaisePropertyChanged("DeviceState");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        public string Ident {
            get {
                return this.identField;
            }
            set {
                this.identField = value;
                this.RaisePropertyChanged("Ident");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
                this.RaisePropertyChanged("code");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string guid {
            get {
                return this.guidField;
            }
            set {
                this.guidField = value;
                this.RaisePropertyChanged("guid");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool loaded {
            get {
                return this.loadedField;
            }
            set {
                this.loadedField = value;
                this.RaisePropertyChanged("loaded");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool loadedSpecified {
            get {
                return this.loadedFieldSpecified;
            }
            set {
                this.loadedFieldSpecified = value;
                this.RaisePropertyChanged("loadedSpecified");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DevicesDeviceDeviceState : object, System.ComponentModel.INotifyPropertyChanged {
        
        private DevicesDeviceDeviceStatePaperStatus paperStatusField;
        
        private DevicesDeviceDeviceStateLogicStatus logicStatusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DevicesDeviceDeviceStatePaperStatus PaperStatus {
            get {
                return this.paperStatusField;
            }
            set {
                this.paperStatusField = value;
                this.RaisePropertyChanged("PaperStatus");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DevicesDeviceDeviceStateLogicStatus LogicStatus {
            get {
                return this.logicStatusField;
            }
            set {
                this.logicStatusField = value;
                this.RaisePropertyChanged("LogicStatus");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DevicesDeviceDeviceStatePaperStatus : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool paperOutField;
        
        private bool paperOutFieldSpecified;
        
        private bool paperLowField;
        
        private bool paperLowFieldSpecified;
        
        private bool doorOpenField;
        
        private bool doorOpenFieldSpecified;
        
        private bool paperOtherField;
        
        private bool paperOtherFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool PaperOut {
            get {
                return this.paperOutField;
            }
            set {
                this.paperOutField = value;
                this.RaisePropertyChanged("PaperOut");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PaperOutSpecified {
            get {
                return this.paperOutFieldSpecified;
            }
            set {
                this.paperOutFieldSpecified = value;
                this.RaisePropertyChanged("PaperOutSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool PaperLow {
            get {
                return this.paperLowField;
            }
            set {
                this.paperLowField = value;
                this.RaisePropertyChanged("PaperLow");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PaperLowSpecified {
            get {
                return this.paperLowFieldSpecified;
            }
            set {
                this.paperLowFieldSpecified = value;
                this.RaisePropertyChanged("PaperLowSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool DoorOpen {
            get {
                return this.doorOpenField;
            }
            set {
                this.doorOpenField = value;
                this.RaisePropertyChanged("DoorOpen");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DoorOpenSpecified {
            get {
                return this.doorOpenFieldSpecified;
            }
            set {
                this.doorOpenFieldSpecified = value;
                this.RaisePropertyChanged("DoorOpenSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool PaperOther {
            get {
                return this.paperOtherField;
            }
            set {
                this.paperOtherField = value;
                this.RaisePropertyChanged("PaperOther");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PaperOtherSpecified {
            get {
                return this.paperOtherFieldSpecified;
            }
            set {
                this.paperOtherFieldSpecified = value;
                this.RaisePropertyChanged("PaperOtherSpecified");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DevicesRes))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RK7QueryResult : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string serverVersionField;
        
        private string xmlVersionField;
        
        private string netNameField;
        
        private string cMDField;
        
        private RK7QueryResultStatus statusField;
        
        private string rK7ErrorNField;
        
        private string errorTextField;
        
        private string workTimeField;
        
        private System.DateTime dateTimeField;
        
        private string processedField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string ServerVersion {
            get {
                return this.serverVersionField;
            }
            set {
                this.serverVersionField = value;
                this.RaisePropertyChanged("ServerVersion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        public string XmlVersion {
            get {
                return this.xmlVersionField;
            }
            set {
                this.xmlVersionField = value;
                this.RaisePropertyChanged("XmlVersion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NetName {
            get {
                return this.netNameField;
            }
            set {
                this.netNameField = value;
                this.RaisePropertyChanged("NetName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="token")]
        public string CMD {
            get {
                return this.cMDField;
            }
            set {
                this.cMDField = value;
                this.RaisePropertyChanged("CMD");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public RK7QueryResultStatus Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("Status");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        public string RK7ErrorN {
            get {
                return this.rK7ErrorNField;
            }
            set {
                this.rK7ErrorNField = value;
                this.RaisePropertyChanged("RK7ErrorN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string ErrorText {
            get {
                return this.errorTextField;
            }
            set {
                this.errorTextField = value;
                this.RaisePropertyChanged("ErrorText");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string WorkTime {
            get {
                return this.workTimeField;
            }
            set {
                this.workTimeField = value;
                this.RaisePropertyChanged("WorkTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime DateTime {
            get {
                return this.dateTimeField;
            }
            set {
                this.dateTimeField = value;
                this.RaisePropertyChanged("DateTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string Processed {
            get {
                return this.processedField;
            }
            set {
                this.processedField = value;
                this.RaisePropertyChanged("Processed");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum RK7QueryResultStatus {
        
        /// <remarks/>
        Ok,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("No changes")]
        Nochanges,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Execution Started")]
        ExecutionStarted,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Query Parse Error")]
        QueryParseError,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Bad Query Parameters")]
        BadQueryParameters,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Query Executing Error")]
        QueryExecutingError,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Result Writing Error")]
        ResultWritingError,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DevicesDeviceDeviceStateLogicStatus : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool fisc24OutField;
        
        private bool fisc24OutFieldSpecified;
        
        private bool eKLZNearEndField;
        
        private bool eKLZNearEndFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool Fisc24Out {
            get {
                return this.fisc24OutField;
            }
            set {
                this.fisc24OutField = value;
                this.RaisePropertyChanged("Fisc24Out");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Fisc24OutSpecified {
            get {
                return this.fisc24OutFieldSpecified;
            }
            set {
                this.fisc24OutFieldSpecified = value;
                this.RaisePropertyChanged("Fisc24OutSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool EKLZNearEnd {
            get {
                return this.eKLZNearEndField;
            }
            set {
                this.eKLZNearEndField = value;
                this.RaisePropertyChanged("EKLZNearEnd");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EKLZNearEndSpecified {
            get {
                return this.eKLZNearEndFieldSpecified;
            }
            set {
                this.eKLZNearEndFieldSpecified = value;
                this.RaisePropertyChanged("EKLZNearEndSpecified");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
