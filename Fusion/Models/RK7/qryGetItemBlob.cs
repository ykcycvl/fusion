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
namespace RK7_qryGetItemBlob {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class RK7Query : object, System.ComponentModel.INotifyPropertyChanged {
        
        private GetItemBlob[] itemsField;
        
        private ItemsChoiceType[] itemsElementNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RK7CMD", typeof(GetItemBlob), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("RK7Command", typeof(GetItemBlob), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlElementAttribute("RK7Command2", typeof(GetItemBlob), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public GetItemBlob[] Items {
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
    public partial class GetItemBlob : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string cMDField;
        
        private refName refNameField;
        
        private string refItemIdentField;
        
        private string refItemGUIDField;
        
        private string refBlobNameField;
        
        private bool encodeBase64Field;
        
        private bool unpackedBlobField;
        
        public GetItemBlob() {
            this.cMDField = "GetItemBlob";
            this.encodeBase64Field = true;
            this.unpackedBlobField = false;
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
        public string RefBlobName {
            get {
                return this.refBlobNameField;
            }
            set {
                this.refBlobNameField = value;
                this.RaisePropertyChanged("RefBlobName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool EncodeBase64 {
            get {
                return this.encodeBase64Field;
            }
            set {
                this.encodeBase64Field = value;
                this.RaisePropertyChanged("EncodeBase64");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool UnpackedBlob {
            get {
                return this.unpackedBlobField;
            }
            set {
                this.unpackedBlobField = value;
                this.RaisePropertyChanged("UnpackedBlob");
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
        ClockRecs,
        
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
        DefferedSync,
        
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
        MenuItemsHierarchies,
        
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
        PersonList,
        
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
        
        /// <remarks/>
        Visits,
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