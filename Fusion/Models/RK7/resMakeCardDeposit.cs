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
namespace RK7_resMakeCardDeposit {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute("RK7QueryResult", Namespace="", IsNullable=false)]
    public partial class RK7QueryResult1 : RK7QueryResult {
        
        private RK7QueryResultCardInfo cardInfoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RK7QueryResultCardInfo CardInfo {
            get {
                return this.cardInfoField;
            }
            set {
                this.cardInfoField = value;
                this.RaisePropertyChanged("CardInfo");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class RK7QueryResultCardInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private resRefItem discountField;
        
        private resRefItem bonusTypeField;
        
        private resRefItem defaulterField;
        
        private RK7QueryResultCardInfoCurrency[] currencyField;
        
        private string cardCodeField;
        
        private string holderField;
        
        private string maxAmountField;
        
        private string amountField;
        
        private string maxDiscField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem Discount {
            get {
                return this.discountField;
            }
            set {
                this.discountField = value;
                this.RaisePropertyChanged("Discount");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem BonusType {
            get {
                return this.bonusTypeField;
            }
            set {
                this.bonusTypeField = value;
                this.RaisePropertyChanged("BonusType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public resRefItem Defaulter {
            get {
                return this.defaulterField;
            }
            set {
                this.defaulterField = value;
                this.RaisePropertyChanged("Defaulter");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Currency", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public RK7QueryResultCardInfoCurrency[] Currency {
            get {
                return this.currencyField;
            }
            set {
                this.currencyField = value;
                this.RaisePropertyChanged("Currency");
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
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string holder {
            get {
                return this.holderField;
            }
            set {
                this.holderField = value;
                this.RaisePropertyChanged("holder");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string maxAmount {
            get {
                return this.maxAmountField;
            }
            set {
                this.maxAmountField = value;
                this.RaisePropertyChanged("maxAmount");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string amount {
            get {
                return this.amountField;
            }
            set {
                this.amountField = value;
                this.RaisePropertyChanged("amount");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string maxDisc {
            get {
                return this.maxDiscField;
            }
            set {
                this.maxDiscField = value;
                this.RaisePropertyChanged("maxDisc");
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
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class RK7QueryResultCardInfoCurrency : resRefItem {
        
        private string subaccField;
        
        private string maxAmountField;
        
        private string amountField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string subacc {
            get {
                return this.subaccField;
            }
            set {
                this.subaccField = value;
                this.RaisePropertyChanged("subacc");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string maxAmount {
            get {
                return this.maxAmountField;
            }
            set {
                this.maxAmountField = value;
                this.RaisePropertyChanged("maxAmount");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="nonNegativeInteger")]
        public string amount {
            get {
                return this.amountField;
            }
            set {
                this.amountField = value;
                this.RaisePropertyChanged("amount");
            }
        }
    }
}
