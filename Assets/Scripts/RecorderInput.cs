using System.Collections;
using System.Collections.Generic;
using Niantic.ARDK.AR.Scanning;
using Niantic.Lightship.AR.Scanning;
using UnityEngine;

public class RecorderInput : MonoBehaviour
{
    [SerializeField] private ARScanningManager _arScanningManager;

    public async void StopRecordingAndExport()
    {

        // save the recording with SaveScan()
        // use ScanStore() to get a reference to it, then ScanArchiveBuilder() to export it
        // output the path to the playback recording as a debug message
        string scanId = _arScanningManager.GetCurrentScanId();
        await _arScanningManager.SaveScan();
        var savedScan = _arScanningManager.GetScanStore().GetSavedScans().Find(scan => scan.ScanId == scanId);
        ScanArchiveBuilder builder = new ScanArchiveBuilder(savedScan, new UploadUserInfo());
        while (builder.HasMoreChunks())
        {
            var task = builder.CreateTaskToGetNextChunk();
            task.Start();
            await task;
            Debug.Log(task.Result);   // <- this is the path to the playback recording.
        }
        _arScanningManager.enabled = false;
    }

    public void StartRecording()
    {
        _arScanningManager.enabled = true;
    }

}