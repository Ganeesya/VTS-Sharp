using System;

namespace VTS.Models {

    [System.Serializable]
    public class VTSMessageData
    {
        public string apiName { get; set; } = "VTubeStudioPublicAPI";
        public string apiVersion { get; set; } = "1.0";
        public long timestamp { get; set; } = 0L;
        public string requestID { get; set; } = Guid.NewGuid().ToString();
        public string messageType { get; set; }="";
    }
    [System.Serializable]
    public class VTSErrorData : VTSMessageData{
         public VTSErrorData(){
            this.messageType = "APIError";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public ErrorID errorID { get; set; }
            public string message { get; set; }
        }
    }

    [System.Serializable]
    public class VTSStateData : VTSMessageData{
        public VTSStateData(){
            this.messageType = "APIStateRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data{
            public bool active { get; set; }
            public string vTubeStudioVersion { get; set; }
            public bool currentSessionAuthenticated { get; set; }
        }
    }

    [System.Serializable]
    public class VTSAuthData : VTSMessageData{
        public VTSAuthData(){
            this.messageType = "AuthenticationTokenRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public string pluginName { get; set; }
            public string pluginDeveloper { get; set; }
            public string pluginIcon { get; set; }
            public string authenticationToken { get; set; }
            public bool authenticated;
            public string reason;
        }
    }

    [System.Serializable]
    public class VTSStatisticsData : VTSMessageData{
         public VTSStatisticsData(){
            this.messageType = "StatisticsRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public long uptime { get; set; }
            public int framerate { get; set; }
            public int allowedPlugins { get; set; }
            public int connectedPlugins { get; set; }
            public bool startedWithSteam { get; set; }
            public int windowWidth { get; set; }
            public int windowHeight { get; set; }
            public bool windowIsFullscreen { get; set; }
        }
    }

    [System.Serializable]
    public class VTSFolderInfoData : VTSMessageData{
         public VTSFolderInfoData(){
            this.messageType = "VTSFolderInfoRequestuest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public string models { get; set; }
            public string backgrounds { get; set; }
            public string items { get; set; }
            public string config { get; set; }
            public string logs { get; set; }
            public string backup { get; set; }
        }
    }

    [System.Serializable]
    public class VTSModelData {
        public bool modelLoaded { get; set; }
        public string modelName { get; set; }
        public string modelID { get; set; }
        public string vtsModelName { get; set; }
        public string vtsModelIconName { get; set; }
    }

    [System.Serializable]
    public class ModelPosition{
        public float positionX { get; set; } = float.MinValue;
        public float positionY { get; set; } = float.MinValue;
        public float rotation { get; set; } = float.MinValue;
        public float size { get; set; } = float.MinValue;

    }

    [System.Serializable]
    public class VTSCurrentModelData : VTSMessageData{
         public VTSCurrentModelData(){
            this.messageType = "CurrentModelRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data : VTSModelData{
		    public string live2DModelName { get; set; }
            public long modelLoadTime { get; set; }
            public long timeSinceModelLoaded { get; set; }
            public int numberOfLive2DParameters { get; set; }
            public int numberOfLive2DArtmeshes { get; set; }
            public bool hasPhysicsFile { get; set; }
            public int numberOfTextures { get; set; }
            public int textureResolution { get; set; }
            public ModelPosition modelPosition { get; set; }

        }
    }

    [System.Serializable]
    public class VTSAvailableModelsData : VTSMessageData{
         public VTSAvailableModelsData(){
            this.messageType = "AvailableModelsRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public int numberOfModels { get; set; }
            public VTSModelData[] availableModels { get; set; }
        }
    }

    [System.Serializable]
    public class VTSModelLoadData : VTSMessageData{
        public VTSModelLoadData(){
            this.messageType = "ModelLoadRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public string modelID { get; set; }
        }
    }

    [System.Serializable]
    public class VTSMoveModelData : VTSMessageData{
        public VTSMoveModelData(){
            this.messageType = "MoveModelRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data : ModelPosition {
            public float timeInSeconds { get; set; }
            public bool valuesAreRelativeToModel { get; set; }
        }
    }

    [System.Serializable]
    public class HotkeyData {
        public string name { get; set; }
        public HotkeyAction type { get; set; }
        public string file { get; set; }
        public string hotkeyID { get; set; }
    }

    [System.Serializable]
    public class VTSHotkeysInCurrentModelData : VTSMessageData{
        public VTSHotkeysInCurrentModelData(){
            this.messageType = "HotkeysInCurrentModelRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public bool modelLoaded { get; set; }
            public string modelName { get; set; }
            public string modelID { get; set; }
            public HotkeyData[] availableHotkeys { get; set; }
        }
    }

    [System.Serializable]
    public class VTSHotkeyTriggerData : VTSMessageData{
        public VTSHotkeyTriggerData(){
            this.messageType = "HotkeyTriggerRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public string hotkeyID { get; set; }
        }
    }

    [System.Serializable]
    public class VTSArtMeshListData : VTSMessageData{
        public VTSArtMeshListData(){
            this.messageType = "ArtMeshListRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public bool modelLoaded { get; set; }
            public int numberOfArtMeshNames { get; set; }
            public int numberOfArtMeshTags { get; set; }
            public string[] artMeshNames { get; set; }
            public string [] artMeshTags { get; set; }
        }
    }

    // must be from 1-255
    [System.Serializable]
    public class ColorTint {
        public byte colorR { get; set; }
        public byte colorG { get; set; }
        public byte colorB { get; set; }
        public byte colorA { get; set; }

        /// <summary>
        /// Converts the color into a Unity color struct.
        /// </summary>
        /// <returns></returns>
        public UnityEngine.Color32 toColor32(){
            return new UnityEngine.Color32(colorR, colorG, colorB, colorA);
        }

        /// <summary>
        /// Loads color data from a Unity color struct
        /// </summary>
        /// <param name="color"></param>
        public void fromColor32(UnityEngine.Color32 color){
            this.colorA = color.a;
            this.colorB = color.b;
            this.colorG = color.g;
            this.colorR = color.r;
        }
    }

    [System.Serializable]
    public class ArtMeshColorTint : ColorTint{
        public float mixWithSceneLightingColor = 1.0f;
    }

    [System.Serializable]
    public class ArtMeshMatcher {
        public bool tintAll { get; set; } = true;
        public int[] artMeshNumber { get; set; }
        public string[] nameExact { get; set; }
        public string[] nameContains { get; set; }
        public string[] tagExact { get; set; }
        public string[] tagContains { get; set; }
    }

    [System.Serializable]
    public class VTSColorTintData : VTSMessageData{
        public VTSColorTintData(){
            this.messageType = "ColorTintRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public ArtMeshColorTint colorTint { get; set; }
            public ArtMeshMatcher artMeshMatcher { get; set; }
            public int matchedArtMeshes { get; set; }
        }
    }

    [System.Serializable]
    public class ColorCapturePart : ColorTint {
        public bool active;
    }

    [System.Serializable]
    public class VTSSceneColorOverlayData : VTSMessageData{
        public VTSSceneColorOverlayData(){
            this.messageType = "SceneColorOverlayInfoRequest";
            this.data = new Data();
        }
        public Data data;

        [System.Serializable]
        public class Data {
            public bool active;
            public bool itemsIncluded;
            public bool isWindowCapture;
            public int baseBrightness;
            public int colorBoost;
            public int smoothing;
            public int colorOverlayR;
            public int colorOverlayG;
            public int colorOverlayB;
            public ColorCapturePart leftCapturePart;
            public ColorCapturePart middleCapturePart;
            public ColorCapturePart rightCapturePart;
        }
    }

    [System.Serializable]
    public class VTSFaceFoundData : VTSMessageData{
        public VTSFaceFoundData(){
            this.messageType = "FaceFoundRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public bool found { get; set; }
        }
    }

    [System.Serializable]
    public class VTSParameter {
        public string name { get; set; }
        public string addedBy { get; set; }
        public float value { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        public float defaultValue { get; set; }
    }

    [System.Serializable]
    public class VTSInputParameterListData : VTSMessageData{
        public VTSInputParameterListData(){
            this.messageType = "InputParameterListRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public bool modelLoaded { get; set; }
            public string modelName { get; set; }
            public string modelID { get; set; }
            public VTSParameter[] customParameters { get; set; }
            public VTSParameter[] defaultParameters { get; set; }
        }
    }

    [System.Serializable]
    public class VTSParameterValueData : VTSMessageData{
        public VTSParameterValueData(){
            this.messageType = "ParameterValueRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data : VTSParameter {}
    }

    [System.Serializable]
    public class VTSLive2DParameterListData : VTSMessageData{
        public VTSLive2DParameterListData(){
            this.messageType = "Live2DParameterListRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public bool modelLoaded { get; set; }
            public string modelName { get; set; }
            public string modelID { get; set; }
            public VTSParameter[] parameters { get; set; }
        }
    }

    [System.Serializable]
    public class VTSCustomParameter {
        // 4-32 characters, alphanumeric
        public string parameterName { get; set; }
        public string explanation { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        public float defaultValue { get; set; }
    }

    [System.Serializable]
    public class VTSParameterCreationData : VTSMessageData{
        public VTSParameterCreationData(){
            this.messageType = "ParameterCreationRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data : VTSCustomParameter {}
    }

    [System.Serializable]
    public class VTSParameterDeletionData : VTSMessageData{
        public VTSParameterDeletionData(){
            this.messageType = "ParameterDeletionRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public string parameterName { get; set; }
        }
    }

    [System.Serializable]
    public class VTSParameterInjectionValue{
        public string id { get; set; }
        public float value { get; set; } = float.MinValue;
        public float weight { get; set; } = float.MinValue;
    }

    [System.Serializable]
    public class VTSInjectParameterData : VTSMessageData{
        public VTSInjectParameterData(){
            this.messageType = "InjectParameterDataRequest";
            this.data = new Data();
        }
        public Data data { get; set; }

        [System.Serializable]
        public class Data {
            public VTSParameterInjectionValue[] parameterValues { get; set; }
        }
    }

    [System.Serializable]
    public class VTSStateBroadcastData : VTSMessageData{
        public VTSStateBroadcastData(){
            this.messageType = "VTubeStudioAPIStateBroadcast";
            this.data = new Data();
        }
        public Data data;

        [System.Serializable]
        public class Data {
            public bool active;
            public int port;
            public string instanceID;
            public string windowTitle;
        }
    }
}

