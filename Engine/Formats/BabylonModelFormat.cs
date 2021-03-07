using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sanjigen.Engine.Formats
{
    public record BabylonModelFormat
    {
        [JsonPropertyName("autoClear")]
        public bool AutoClear { get; init; }

        [JsonPropertyName("clearColor")]
        public IReadOnlyList<int> ClearColor { get; init; }

        [JsonPropertyName("ambientColor")]
        public IReadOnlyList<int> AmbientColor { get; init; }

        [JsonPropertyName("gravity")]
        public IReadOnlyList<double> Gravity { get; init; }

        [JsonPropertyName("cameras")]
        public IReadOnlyList<Camera> Cameras { get; init; }

        [JsonPropertyName("activeCamera")]
        public string ActiveCamera { get; init; }

        [JsonPropertyName("lights")]
        public IReadOnlyList<Light> Lights { get; init; }

        [JsonPropertyName("materials")]
        public IReadOnlyList<Material> Materials { get; init; }

        [JsonPropertyName("meshes")]
        public IReadOnlyList<Mesh> Meshes { get; init; }

        [JsonPropertyName("multiMaterials")]
        public IReadOnlyList<object> MultiMaterials { get; init; }


        public record Camera
        {
            [JsonPropertyName("name")]
            public string Name { get; init; }

            [JsonPropertyName("id")]
            public string Id { get; init; }

            [JsonPropertyName("position")]
            public IReadOnlyList<double> Position { get; init; }

            [JsonPropertyName("target")]
            public IReadOnlyList<double> Target { get; init; }

            [JsonPropertyName("fov")]
            public double Fov { get; init; }

            [JsonPropertyName("minZ")]
            public double MinZ { get; init; }

            [JsonPropertyName("maxZ")]
            public int MaxZ { get; init; }

            [JsonPropertyName("speed")]
            public int Speed { get; init; }

            [JsonPropertyName("inertia")]
            public double Inertia { get; init; }

            [JsonPropertyName("checkCollisions")]
            public bool CheckCollisions { get; init; }

            [JsonPropertyName("applyGravity")]
            public bool ApplyGravity { get; init; }

            [JsonPropertyName("ellipsoid")]
            public IReadOnlyList<double> Ellipsoid { get; init; }
        }

        public record Light
        {
            [JsonPropertyName("name")]
            public string Name { get; init; }

            [JsonPropertyName("id")]
            public string Id { get; init; }

            [JsonPropertyName("type")]
            public int Type { get; init; }

            [JsonPropertyName("data")]
            public IReadOnlyList<double> Data { get; init; }

            [JsonPropertyName("intensity")]
            public int Intensity { get; init; }

            [JsonPropertyName("diffuse")]
            public IReadOnlyList<int> Diffuse { get; init; }

            [JsonPropertyName("specular")]
            public IReadOnlyList<int> Specular { get; init; }
        }

        public record DiffuseTexture
        {
            [JsonPropertyName("name")]
            public string Name { get; init; }

            [JsonPropertyName("level")]
            public int Level { get; init; }

            [JsonPropertyName("hasAlpha")]
            public int HasAlpha { get; init; }

            [JsonPropertyName("coordinatesMode")]
            public int CoordinatesMode { get; init; }

            [JsonPropertyName("uOffset")]
            public int UOffset { get; init; }

            [JsonPropertyName("vOffset")]
            public int VOffset { get; init; }

            [JsonPropertyName("uScale")]
            public int UScale { get; init; }

            [JsonPropertyName("vScale")]
            public int VScale { get; init; }

            [JsonPropertyName("uAng")]
            public int UAng { get; init; }

            [JsonPropertyName("vAng")]
            public int VAng { get; init; }

            [JsonPropertyName("wAng")]
            public int WAng { get; init; }

            [JsonPropertyName("wrapU")]
            public bool WrapU { get; init; }

            [JsonPropertyName("wrapV")]
            public bool WrapV { get; init; }

            [JsonPropertyName("coordinatesIndex")]
            public int CoordinatesIndex { get; init; }
        }

        public record Material
        {
            [JsonPropertyName("name")]
            public string Name { get; init; }

            [JsonPropertyName("id")]
            public string Id { get; init; }

            [JsonPropertyName("ambient")]
            public IReadOnlyList<double> Ambient { get; init; }

            [JsonPropertyName("diffuse")]
            public IReadOnlyList<double> Diffuse { get; init; }

            [JsonPropertyName("specular")]
            public IReadOnlyList<double> Specular { get; init; }

            [JsonPropertyName("specularPower")]
            public int SpecularPower { get; init; }

            [JsonPropertyName("emissive")]
            public IReadOnlyList<int> Emissive { get; init; }

            [JsonPropertyName("alpha")]
            public int Alpha { get; init; }

            [JsonPropertyName("backFaceCulling")]
            public bool BackFaceCulling { get; init; }

            [JsonPropertyName("diffuseTexture")]
            public DiffuseTexture DiffuseTexture { get; init; }
        }

        public record SubMesh
        {
            [JsonPropertyName("materialIndex")]
            public int MaterialIndex { get; init; }

            [JsonPropertyName("verticesStart")]
            public int VerticesStart { get; init; }

            [JsonPropertyName("verticesCount")]
            public int VerticesCount { get; init; }

            [JsonPropertyName("indexStart")]
            public int IndexStart { get; init; }

            [JsonPropertyName("indexCount")]
            public int IndexCount { get; init; }
        }

        public record Mesh
        {
            [JsonPropertyName("name")]
            public string Name { get; init; }

            [JsonPropertyName("id")]
            public string Id { get; init; }

            [JsonPropertyName("materialId")]
            public string MaterialId { get; init; }

            [JsonPropertyName("position")]
            public IReadOnlyList<double> Position { get; init; }

            [JsonPropertyName("rotation")]
            public IReadOnlyList<int> Rotation { get; init; }

            [JsonPropertyName("scaling")]
            public IReadOnlyList<int> Scaling { get; init; }

            [JsonPropertyName("isVisible")]
            public bool IsVisible { get; init; }

            [JsonPropertyName("isEnabled")]
            public bool IsEnabled { get; init; }

            [JsonPropertyName("checkCollisions")]
            public bool CheckCollisions { get; init; }

            [JsonPropertyName("billboardMode")]
            public int BillboardMode { get; init; }

            [JsonPropertyName("uvCount")]
            public int UvCount { get; init; }

            [JsonPropertyName("vertices")]
            public IReadOnlyList<double> Vertices { get; init; }

            [JsonPropertyName("indices")]
            public IReadOnlyList<int> Indices { get; init; }

            [JsonPropertyName("subMeshes")]
            public IReadOnlyList<SubMesh> SubMeshes { get; init; }
        }
    }
}