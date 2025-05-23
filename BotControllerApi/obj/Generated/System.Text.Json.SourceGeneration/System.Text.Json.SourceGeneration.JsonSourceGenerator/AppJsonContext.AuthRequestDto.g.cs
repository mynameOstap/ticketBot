﻿// <auto-generated/>

#nullable enable annotations
#nullable disable warnings

// Suppress warnings about [Obsolete] member usage in generated code.
#pragma warning disable CS0612, CS0618

namespace BotControllerApi
{
    public partial class AppJsonContext
    {
        private global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<global::BotControllerApi.Models.AuthRequestDto>? _AuthRequestDto;
        
        /// <summary>
        /// Defines the source generated JSON serialization contract metadata for a given type.
        /// </summary>
        #nullable disable annotations // Marking the property type as nullable-oblivious.
        public global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<global::BotControllerApi.Models.AuthRequestDto> AuthRequestDto
        #nullable enable annotations
        {
            get => _AuthRequestDto ??= (global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<global::BotControllerApi.Models.AuthRequestDto>)Options.GetTypeInfo(typeof(global::BotControllerApi.Models.AuthRequestDto));
        }
        
        private global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<global::BotControllerApi.Models.AuthRequestDto> Create_AuthRequestDto(global::System.Text.Json.JsonSerializerOptions options)
        {
            if (!TryGetTypeInfoForRuntimeCustomConverter<global::BotControllerApi.Models.AuthRequestDto>(options, out global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<global::BotControllerApi.Models.AuthRequestDto> jsonTypeInfo))
            {
                var objectInfo = new global::System.Text.Json.Serialization.Metadata.JsonObjectInfoValues<global::BotControllerApi.Models.AuthRequestDto>
                {
                    ObjectCreator = () => new global::BotControllerApi.Models.AuthRequestDto(),
                    ObjectWithParameterizedConstructorCreator = null,
                    PropertyMetadataInitializer = _ => AuthRequestDtoPropInit(options),
                    ConstructorParameterMetadataInitializer = null,
                    ConstructorAttributeProviderFactory = static () => typeof(global::BotControllerApi.Models.AuthRequestDto).GetConstructor(InstanceMemberBindingFlags, binder: null, global::System.Array.Empty<global::System.Type>(), modifiers: null),
                    SerializeHandler = AuthRequestDtoSerializeHandler,
                };
                
                jsonTypeInfo = global::System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreateObjectInfo<global::BotControllerApi.Models.AuthRequestDto>(options, objectInfo);
                jsonTypeInfo.NumberHandling = null;
            }
        
            jsonTypeInfo.OriginatingResolver = this;
            return jsonTypeInfo;
        }

        private static global::System.Text.Json.Serialization.Metadata.JsonPropertyInfo[] AuthRequestDtoPropInit(global::System.Text.Json.JsonSerializerOptions options)
        {
            var properties = new global::System.Text.Json.Serialization.Metadata.JsonPropertyInfo[2];

            var info0 = new global::System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
            {
                IsProperty = true,
                IsPublic = true,
                IsVirtual = false,
                DeclaringType = typeof(global::BotControllerApi.Models.AuthRequestDto),
                Converter = null,
                Getter = static obj => ((global::BotControllerApi.Models.AuthRequestDto)obj).Email,
                Setter = static (obj, value) => ((global::BotControllerApi.Models.AuthRequestDto)obj).Email = value!,
                IgnoreCondition = null,
                HasJsonInclude = false,
                IsExtensionData = false,
                NumberHandling = null,
                PropertyName = "Email",
                JsonPropertyName = null,
                AttributeProviderFactory = static () => typeof(global::BotControllerApi.Models.AuthRequestDto).GetProperty("Email", InstanceMemberBindingFlags, null, typeof(string), global::System.Array.Empty<global::System.Type>(), null),
            };
            
            properties[0] = global::System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo<string>(options, info0);
            properties[0].IsGetNullable = false;
            properties[0].IsSetNullable = false;

            var info1 = new global::System.Text.Json.Serialization.Metadata.JsonPropertyInfoValues<string>
            {
                IsProperty = true,
                IsPublic = true,
                IsVirtual = false,
                DeclaringType = typeof(global::BotControllerApi.Models.AuthRequestDto),
                Converter = null,
                Getter = static obj => ((global::BotControllerApi.Models.AuthRequestDto)obj).Password,
                Setter = static (obj, value) => ((global::BotControllerApi.Models.AuthRequestDto)obj).Password = value!,
                IgnoreCondition = null,
                HasJsonInclude = false,
                IsExtensionData = false,
                NumberHandling = null,
                PropertyName = "Password",
                JsonPropertyName = null,
                AttributeProviderFactory = static () => typeof(global::BotControllerApi.Models.AuthRequestDto).GetProperty("Password", InstanceMemberBindingFlags, null, typeof(string), global::System.Array.Empty<global::System.Type>(), null),
            };
            
            properties[1] = global::System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreatePropertyInfo<string>(options, info1);
            properties[1].IsGetNullable = false;
            properties[1].IsSetNullable = false;

            return properties;
        }

        // Intentionally not a static method because we create a delegate to it. Invoking delegates to instance
        // methods is almost as fast as virtual calls. Static methods need to go through a shuffle thunk.
        private void AuthRequestDtoSerializeHandler(global::System.Text.Json.Utf8JsonWriter writer, global::BotControllerApi.Models.AuthRequestDto? value)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }
            
            writer.WriteStartObject();

            writer.WriteString(PropName_Email, ((global::BotControllerApi.Models.AuthRequestDto)value).Email);
            writer.WriteString(PropName_Password, ((global::BotControllerApi.Models.AuthRequestDto)value).Password);

            writer.WriteEndObject();
        }
    }
}
