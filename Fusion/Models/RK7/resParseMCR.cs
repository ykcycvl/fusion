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
namespace RK7_resParseMCR {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute("RK7QueryResult", Namespace="", IsNullable=false)]
    public partial class RK7QueryResult1 : RK7QueryResult {
        
        private ParseItem[] itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ParseItem[] Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
                this.RaisePropertyChanged("Item");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ParseItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private resRefItem mCRField;
        
        private resRefItem objectField;
        
        private string cardCodeField;
        
        private ParseItemScope scopeField;
        
        private bool scopeFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem MCR {
            get {
                return this.mCRField;
            }
            set {
                this.mCRField = value;
                this.RaisePropertyChanged("MCR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem Object {
            get {
                return this.objectField;
            }
            set {
                this.objectField = value;
                this.RaisePropertyChanged("Object");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string cardCode {
            get {
                return this.cardCodeField;
            }
            set {
                this.cardCodeField = value;
                this.RaisePropertyChanged("cardCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ParseItemScope scope {
            get {
                return this.scopeField;
            }
            set {
                this.scopeField = value;
                this.RaisePropertyChanged("scope");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool scopeSpecified {
            get {
                return this.scopeFieldSpecified;
            }
            set {
                this.scopeFieldSpecified = value;
                this.RaisePropertyChanged("scopeSpecified");
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
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(resEmployeeItem))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class resRefItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idField;
        
        private string codeField;
        
        private string nameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
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
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("name");
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
    public partial class resEmployeeItem : resRefItem {
        
        private resRefItem roleField;
        
        /// <remarks/>
        public resRefItem Role {
            get {
                return this.roleField;
            }
            set {
                this.roleField = value;
                this.RaisePropertyChanged("Role");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ParseItemScope {
        
        /// <remarks/>
        Currency,
        
        /// <remarks/>
        Employee,
        
        /// <remarks/>
        Discount,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Entrance card")]
        Entrancecard,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Function Key")]
        FunctionKey,
        
        /// <remarks/>
        Interface,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Menu Item")]
        MenuItem,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Menu Item Barcode")]
        MenuItemBarcode,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Dish Control")]
        DishControl,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Order Type")]
        OrderType,
    }
}
