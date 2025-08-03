using System.Text.Json;
using System.Text.Json.Serialization;
using Tavily.McpServer.ClientSdk.Models.Search;

namespace Tavily.McpServer.ClientSdk.JsonConverter;

/// <summary>
/// A custom JSON converter for serializing and deserializing a list of <see cref="Image"/> objects.
/// Supports both array of objects and array of strings representations.
/// </summary>
internal class ImageJsonConverter : JsonConverter<List<Image>>
{
    /// <summary>
    /// Reads and converts the JSON to a list of <see cref="Image"/> objects.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    /// <param name="typeToConvert">The type to convert.</param>
    /// <param name="options">Options to use for deserialization.</param>
    /// <returns>A list of <see cref="Image"/> objects.</returns>
    /// <exception cref="JsonException">Thrown when the JSON is not in the expected format.</exception>
    public override List<Image> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected start of array for 'images'.");

        var images = new List<Image>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                string url = string.Empty;
                string? description = null;

                while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string? propertyName = reader.GetString();
                        reader.Read(); 

                        if (string.Equals(propertyName, "url", StringComparison.OrdinalIgnoreCase))
                        {
                            url = reader.GetString()!.Trim();
                        }
                        else if (string.Equals(propertyName, "description", StringComparison.OrdinalIgnoreCase))
                        {
                            description = reader.GetString()?.Trim();
                        }
                    }
                }

                images.Add(new Image { Url = url, Description = description });
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                string url = reader.GetString()!.Trim();
                images.Add(new Image { Url = url });
            }
            else
            {
                throw new JsonException($"Unexpected token type when reading image: {reader.TokenType}");
            }
        }

        return images;
    }

    /// <summary>
    /// Writes a list of <see cref="Image"/> objects as JSON.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The list of <see cref="Image"/> objects to write.</param>
    /// <param name="options">Options to use for serialization.</param>
    public override void Write(Utf8JsonWriter writer, List<Image> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var item in value)
        {
            if (!string.IsNullOrEmpty(item.Description))
            {
                writer.WriteStartObject();
                writer.WriteString("url", item.Url);
                writer.WriteString("description", item.Description);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteStringValue(item.Url);
            }
        }
        writer.WriteEndArray();
    }
}
