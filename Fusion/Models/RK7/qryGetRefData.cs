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
namespace RK7_qryGetRefData {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class RK7Query : object, System.ComponentModel.INotifyPropertyChanged {
        
        private GetRefDataCommand[] itemsField;
        
        private ItemsChoiceType[] itemsElementNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RK7CMD", typeof(GetRefDataCommand), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("RK7Command", typeof(GetRefDataCommand), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("RK7Command2", typeof(GetRefDataCommand), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public GetRefDataCommand[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
                this.RaisePropertyChanged("ItemsElementName");
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
    public partial class GetRefDataCommand : object, System.ComponentModel.INotifyPropertyChanged {
        
        private GetRefDataCommandPROPFILTER[] pROPFILTERSField;
        
        private string cMDField;
        
        private refName refNameField;
        
        private string refItemIdentField;
        
        private string refItemGUIDField;
        
        private string propMaskField;
        
        private bool withMacroPropField;
        
        private bool withBlobsDataField;
        
        private bool macroPropTagsField;
        
        private int withChildItemsField;
        
        private bool ignoreEnumsField;
        
        private bool ignoreDefaultsField;
        
        private bool onlyActiveField;
        
        public GetRefDataCommand() {
            this.cMDField = "GetRefData";
            this.withMacroPropField = false;
            this.withBlobsDataField = false;
            this.macroPropTagsField = false;
            this.withChildItemsField = 0;
            this.ignoreEnumsField = false;
            this.ignoreDefaultsField = false;
            this.onlyActiveField = false;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("PROPFILTER", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public GetRefDataCommandPROPFILTER[] PROPFILTERS {
            get {
                return this.pROPFILTERSField;
            }
            set {
                this.pROPFILTERSField = value;
                this.RaisePropertyChanged("PROPFILTERS");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        public refName RefName {
            get {
                return this.refNameField;
            }
            set {
                this.refNameField = value;
                this.RaisePropertyChanged("RefName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        public string RefItemIdent {
            get {
                return this.refItemIdentField;
            }
            set {
                this.refItemIdentField = value;
                this.RaisePropertyChanged("RefItemIdent");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RefItemGUID {
            get {
                return this.refItemGUIDField;
            }
            set {
                this.refItemGUIDField = value;
                this.RaisePropertyChanged("RefItemGUID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PropMask {
            get {
                return this.propMaskField;
            }
            set {
                this.propMaskField = value;
                this.RaisePropertyChanged("PropMask");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool WithMacroProp {
            get {
                return this.withMacroPropField;
            }
            set {
                this.withMacroPropField = value;
                this.RaisePropertyChanged("WithMacroProp");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool WithBlobsData {
            get {
                return this.withBlobsDataField;
            }
            set {
                this.withBlobsDataField = value;
                this.RaisePropertyChanged("WithBlobsData");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool MacroPropTags {
            get {
                return this.macroPropTagsField;
            }
            set {
                this.macroPropTagsField = value;
                this.RaisePropertyChanged("MacroPropTags");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(0)]
        public int WithChildItems {
            get {
                return this.withChildItemsField;
            }
            set {
                this.withChildItemsField = value;
                this.RaisePropertyChanged("WithChildItems");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool IgnoreEnums {
            get {
                return this.ignoreEnumsField;
            }
            set {
                this.ignoreEnumsField = value;
                this.RaisePropertyChanged("IgnoreEnums");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool IgnoreDefaults {
            get {
                return this.ignoreDefaultsField;
            }
            set {
                this.ignoreDefaultsField = value;
                this.RaisePropertyChanged("IgnoreDefaults");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool OnlyActive {
            get {
                return this.onlyActiveField;
            }
            set {
                this.onlyActiveField = value;
                this.RaisePropertyChanged("OnlyActive");
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
    public partial class GetRefDataCommandPROPFILTER : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string nameField;
        
        private string valueField;
        
        private string substringField;
        
        private string maskedField;
        
        private string kindField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Substring {
            get {
                return this.substringField;
            }
            set {
                this.substringField = value;
                this.RaisePropertyChanged("Substring");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        public string Masked {
            get {
                return this.maskedField;
            }
            set {
                this.maskedField = value;
                this.RaisePropertyChanged("Masked");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Kind {
            get {
                return this.kindField;
            }
            set {
                this.kindField = value;
                this.RaisePropertyChanged("Kind");
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
    public enum refName {
        
        /// <remarks/>
        AvailabilitySchedules,
        
        /// <remarks/>
        AwardsPenaltiesGroups,
        
        /// <remarks/>
        AwardsPenaltiesTypes,
        
        /// <remarks/>
        BonusTypes,
        
        /// <remarks/>
        Brigades,
        
        /// <remarks/>
        Cashes,
        
        /// <remarks/>
        CashGroups,
        
        /// <remarks/>
        CashReportDetails,
        
        /// <remarks/>
        CashServDataStatuses,
        
        /// <remarks/>
        CategList,
        
        /// <remarks/>
        ChangeableOrderTypes,
        
        /// <remarks/>
        CheckTables,
        
        /// <remarks/>
        ClassificatorGroups,
        
        /// <remarks/>
        ClassInfoGroups,
        
        /// <remarks/>
        ClassInfos,
        
        /// <remarks/>
        ColorMappings,
        
        /// <remarks/>
        ColorSchemes,
        
        /// <remarks/>
        Consumators,
        
        /// <remarks/>
        ConsumTypes,
        
        /// <remarks/>
        Currencies,
        
        /// <remarks/>
        CurrencyFaceValues,
        
        /// <remarks/>
        CurrencyTypes,
        
        /// <remarks/>
        DefaulterTypes,
        
        /// <remarks/>
        DepositCollectReasons,
        
        /// <remarks/>
        DeviceDataLookUpItems,
        
        /// <remarks/>
        Devices,
        
        /// <remarks/>
        DiscountCompositions,
        
        /// <remarks/>
        DiscountDetails,
        
        /// <remarks/>
        Discounts,
        
        /// <remarks/>
        DiscountTypes,
        
        /// <remarks/>
        DisplayResolutions,
        
        /// <remarks/>
        DocumentMaketSchemeLinks,
        
        /// <remarks/>
        Documents,
        
        /// <remarks/>
        DosingDevices,
        
        /// <remarks/>
        EmployeeGroupDetails,
        
        /// <remarks/>
        EmployeeGroups,
        
        /// <remarks/>
        Employees,
        
        /// <remarks/>
        EntranceCardTypes,
        
        /// <remarks/>
        EnumsTypesDatas,
        
        /// <remarks/>
        EnumsTypesInfos,
        
        /// <remarks/>
        Filters,
        
        /// <remarks/>
        FiscDevParams,
        
        /// <remarks/>
        FormDetails,
        
        /// <remarks/>
        Forms,
        
        /// <remarks/>
        FormSchemeChilds,
        
        /// <remarks/>
        FormSchemes,
        
        /// <remarks/>
        FunctionKeyGroups,
        
        /// <remarks/>
        FunctionKeys,
        
        /// <remarks/>
        FundsAccountingPlaces,
        
        /// <remarks/>
        GeneratedPropDatas,
        
        /// <remarks/>
        GeneratedPropGroups,
        
        /// <remarks/>
        GeneratedPropTypes,
        
        /// <remarks/>
        GuestTypes,
        
        /// <remarks/>
        HallPlans,
        
        /// <remarks/>
        ImageList,
        
        /// <remarks/>
        ImageNodes,
        
        /// <remarks/>
        InpDevTypes,
        
        /// <remarks/>
        KBDLayouts,
        
        /// <remarks/>
        KBDTypes,
        
        /// <remarks/>
        Kurses,
        
        /// <remarks/>
        LinkedSystemConfigs,
        
        /// <remarks/>
        LinkedSystemTypes,
        
        /// <remarks/>
        Makets,
        
        /// <remarks/>
        MaketSchemeDetails,
        
        /// <remarks/>
        MaketSchemes,
        
        /// <remarks/>
        McrAlgorithms,
        
        /// <remarks/>
        MenuItems,
        
        /// <remarks/>
        Modifiers,
        
        /// <remarks/>
        ModiGroups,
        
        /// <remarks/>
        ModiSchemeDetails,
        
        /// <remarks/>
        ModiSchemes,
        
        /// <remarks/>
        OlapCubes,
        
        /// <remarks/>
        OlapCubeSchemes,
        
        /// <remarks/>
        OperationClasses,
        
        /// <remarks/>
        Operations,
        
        /// <remarks/>
        OrderVoids,
        
        /// <remarks/>
        ParameterExceptions,
        
        /// <remarks/>
        ParameterHierarhies,
        
        /// <remarks/>
        Parameters,
        
        /// <remarks/>
        PeriodDetails,
        
        /// <remarks/>
        Periods,
        
        /// <remarks/>
        PriceConstants,
        
        /// <remarks/>
        PriceConstantTypes,
        
        /// <remarks/>
        Prices,
        
        /// <remarks/>
        PriceTypes,
        
        /// <remarks/>
        PrinterPurposes,
        
        /// <remarks/>
        RateClasses,
        
        /// <remarks/>
        RefLinks,
        
        /// <remarks/>
        ReportingServers,
        
        /// <remarks/>
        RestaurantConcepts,
        
        /// <remarks/>
        RestaurantRegions,
        
        /// <remarks/>
        Restaurants,
        
        /// <remarks/>
        RightGroups,
        
        /// <remarks/>
        Rights,
        
        /// <remarks/>
        Roles,
        
        /// <remarks/>
        Scripts,
        
        /// <remarks/>
        ScriptTypes,
        
        /// <remarks/>
        SelectorDetails,
        
        /// <remarks/>
        SelectorGroups,
        
        /// <remarks/>
        SelectorHierarhies,
        
        /// <remarks/>
        Selectors,
        
        /// <remarks/>
        SelectorTypes,
        
        /// <remarks/>
        ServiceChecks,
        
        /// <remarks/>
        ServiceSchemes,
        
        /// <remarks/>
        ServingPositions,
        
        /// <remarks/>
        TableAttributes,
        
        /// <remarks/>
        TableGroups,
        
        /// <remarks/>
        Tables,
        
        /// <remarks/>
        TariffDetails,
        
        /// <remarks/>
        TarifficationTypes,
        
        /// <remarks/>
        TaskSetters,
        
        /// <remarks/>
        TaxDishRules,
        
        /// <remarks/>
        TaxDishTypes,
        
        /// <remarks/>
        Taxes,
        
        /// <remarks/>
        TaxPayRules,
        
        /// <remarks/>
        TaxPayTypes,
        
        /// <remarks/>
        TaxRates,
        
        /// <remarks/>
        TradeGroupDetails,
        
        /// <remarks/>
        TradeGroups,
        
        /// <remarks/>
        UnchangeableOrderTypes,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema=false)]
    public enum ItemsChoiceType {
        
        /// <remarks/>
        RK7CMD,
        
        /// <remarks/>
        RK7Command,
        
        /// <remarks/>
        RK7Command2,
    }
}
