<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="27844ced-a126-486a-ad68-977fcfb447ce" namespace="KerbalData.Configuration" xmlSchemaNamespace="urn:KerbalData.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="ApiConfig" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="kerbalData">
      <elementProperties>
        <elementProperty name="Processors" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="processors" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/27844ced-a126-486a-ad68-977fcfb447ce/ProcessorsConfig" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="ProcessorsConfig" xmlItemName="processor" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/27844ced-a126-486a-ad68-977fcfb447ce/ProcessorConfig" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="ProcessorConfig">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/27844ced-a126-486a-ad68-977fcfb447ce/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Index" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="index" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/27844ced-a126-486a-ad68-977fcfb447ce/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="ModelType" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="modelType" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/27844ced-a126-486a-ad68-977fcfb447ce/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Serializer" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="serializer" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/27844ced-a126-486a-ad68-977fcfb447ce/SerializerConfig" />
          </type>
        </elementProperty>
        <elementProperty name="Converter" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="converter" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/27844ced-a126-486a-ad68-977fcfb447ce/ConverterConfig" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="SerializerConfig">
      <attributeProperties>
        <attributeProperty name="Type" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="type" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/27844ced-a126-486a-ad68-977fcfb447ce/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="ConverterConfig">
      <attributeProperties>
        <attributeProperty name="Type" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="type" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/27844ced-a126-486a-ad68-977fcfb447ce/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>