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
namespace RK7_resGetReceiptList {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute("RK7QueryResult", Namespace="", IsNullable=false)]
    public partial class RK7QueryResult1 : RK7QueryResult {
        
        private RK7QueryResultReceiptsList receiptsListField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RK7QueryResultReceiptsList ReceiptsList {
            get {
                return this.receiptsListField;
            }
            set {
                this.receiptsListField = value;
                this.RaisePropertyChanged("ReceiptsList");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class RK7QueryResultReceiptsList : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Item[] receiptField;
        
        private string countField;
        
        private string lastversionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Receipt", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Item[] Receipt {
            get {
                return this.receiptField;
            }
            set {
                this.receiptField = value;
                this.RaisePropertyChanged("Receipt");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        public string count {
            get {
                return this.countField;
            }
            set {
                this.countField = value;
                this.RaisePropertyChanged("count");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        public string lastversion {
            get {
                return this.lastversionField;
            }
            set {
                this.lastversionField = value;
                this.RaisePropertyChanged("lastversion");
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
    public partial class Item : object, System.ComponentModel.INotifyPropertyChanged {
        
        private orderElement orderField;
        
        private resRefItem closeStationField;
        
        private resRefItem printStationField;
        
        private resRefItem cashierField;
        
        private resRefItem deleteManagerField;
        
        private resRefItem deleteReasonField;
        
        private string line_guidField;
        
        private int checknumField;
        
        private int sumField;
        
        private bool sumFieldSpecified;
        
        private int stateField;
        
        private int seatField;
        
        private bool seatFieldSpecified;
        
        private System.DateTime printtimeField;
        
        private bool printtimeFieldSpecified;
        
        private System.DateTime starttimeField;
        
        private bool starttimeFieldSpecified;
        
        private System.DateTime billtimeField;
        
        private bool billtimeFieldSpecified;
        
        private System.DateTime deletetimeField;
        
        private bool deletetimeFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public orderElement Order {
            get {
                return this.orderField;
            }
            set {
                this.orderField = value;
                this.RaisePropertyChanged("Order");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem CloseStation {
            get {
                return this.closeStationField;
            }
            set {
                this.closeStationField = value;
                this.RaisePropertyChanged("CloseStation");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem PrintStation {
            get {
                return this.printStationField;
            }
            set {
                this.printStationField = value;
                this.RaisePropertyChanged("PrintStation");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem Cashier {
            get {
                return this.cashierField;
            }
            set {
                this.cashierField = value;
                this.RaisePropertyChanged("Cashier");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem DeleteManager {
            get {
                return this.deleteManagerField;
            }
            set {
                this.deleteManagerField = value;
                this.RaisePropertyChanged("DeleteManager");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem DeleteReason {
            get {
                return this.deleteReasonField;
            }
            set {
                this.deleteReasonField = value;
                this.RaisePropertyChanged("DeleteReason");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string line_guid {
            get {
                return this.line_guidField;
            }
            set {
                this.line_guidField = value;
                this.RaisePropertyChanged("line_guid");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int checknum {
            get {
                return this.checknumField;
            }
            set {
                this.checknumField = value;
                this.RaisePropertyChanged("checknum");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int sum {
            get {
                return this.sumField;
            }
            set {
                this.sumField = value;
                this.RaisePropertyChanged("sum");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sumSpecified {
            get {
                return this.sumFieldSpecified;
            }
            set {
                this.sumFieldSpecified = value;
                this.RaisePropertyChanged("sumSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int state {
            get {
                return this.stateField;
            }
            set {
                this.stateField = value;
                this.RaisePropertyChanged("state");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int seat {
            get {
                return this.seatField;
            }
            set {
                this.seatField = value;
                this.RaisePropertyChanged("seat");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool seatSpecified {
            get {
                return this.seatFieldSpecified;
            }
            set {
                this.seatFieldSpecified = value;
                this.RaisePropertyChanged("seatSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime printtime {
            get {
                return this.printtimeField;
            }
            set {
                this.printtimeField = value;
                this.RaisePropertyChanged("printtime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool printtimeSpecified {
            get {
                return this.printtimeFieldSpecified;
            }
            set {
                this.printtimeFieldSpecified = value;
                this.RaisePropertyChanged("printtimeSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime starttime {
            get {
                return this.starttimeField;
            }
            set {
                this.starttimeField = value;
                this.RaisePropertyChanged("starttime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool starttimeSpecified {
            get {
                return this.starttimeFieldSpecified;
            }
            set {
                this.starttimeFieldSpecified = value;
                this.RaisePropertyChanged("starttimeSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime billtime {
            get {
                return this.billtimeField;
            }
            set {
                this.billtimeField = value;
                this.RaisePropertyChanged("billtime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool billtimeSpecified {
            get {
                return this.billtimeFieldSpecified;
            }
            set {
                this.billtimeFieldSpecified = value;
                this.RaisePropertyChanged("billtimeSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime deletetime {
            get {
                return this.deletetimeField;
            }
            set {
                this.deletetimeField = value;
                this.RaisePropertyChanged("deletetime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deletetimeSpecified {
            get {
                return this.deletetimeFieldSpecified;
            }
            set {
                this.deletetimeFieldSpecified = value;
                this.RaisePropertyChanged("deletetimeSpecified");
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
    public partial class orderElement : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string guidField;
        
        private string visitField;
        
        private string orderIdentField;
        
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
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        public string visit {
            get {
                return this.visitField;
            }
            set {
                this.visitField = value;
                this.RaisePropertyChanged("visit");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        public string orderIdent {
            get {
                return this.orderIdentField;
            }
            set {
                this.orderIdentField = value;
                this.RaisePropertyChanged("orderIdent");
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
}
