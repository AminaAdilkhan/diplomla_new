using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiClient
{
    private static string baseUrl = "https://api.example.com/";
    private static string jwtToken = "";

    public static void SetToken(string token)
    {
        jwtToken = token;
    }

    public static async Task<T> Get<T>(string endpoint)
    {
        using var request = UnityWebRequest.Get(baseUrl + endpoint);
        ApplyHeaders(request);

        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            return default;
        }

        return JsonUtility.FromJson<T>(request.downloadHandler.text);
    }

    public static async Task<T> Post<T>(string endpoint, object body)
    {
        var json = JsonUtility.ToJson(body);
        var request = new UnityWebRequest(baseUrl + endpoint, "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        ApplyHeaders(request);

        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            return default;
        }

        return JsonUtility.FromJson<T>(request.downloadHandler.text);
    }

    private static void ApplyHeaders(UnityWebRequest request)
    {
        if (!string.IsNullOrEmpty(jwtToken))
            request.SetRequestHeader("Authorization", $"Bearer {jwtToken}");
    }
}
