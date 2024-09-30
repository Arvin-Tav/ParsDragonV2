//webkitURL is deprecated but nevertheless
URL = window.URL || window.webkitURL;

var gumStream; 						//stream from getUserMedia()
var rec; 							//Recorder.js object
var input; 							//MediaStreamAudioSourceNode we'll be recording

// shim for AudioContext when it's not avb. 
var AudioContext = window.AudioContext || window.webkitAudioContext;
var audioContext //audio context to help us record
var ElemId = "";
var BlobFiles = [];

var recordButton = document.getElementById("recordButton");
var stopButton = document.getElementById("stopButton");
var pauseButton = document.getElementById("pauseButton");
var stopAndDeleteButton = document.getElementById("stopAndDeleteButton");
var recordingsList = document.getElementById("recordingsList");
var recordStatus = document.getElementById("recordStatusBar");

//add events to those 2 buttons
recordButton.addEventListener("click", startRecording);
stopButton.addEventListener("click", stopRecording);
stopAndDeleteButton.addEventListener("click", stopAndDeleteRecording);
pauseButton.addEventListener("click", pauseRecording);

function startRecording() {
    console.log("recordButton clicked");

    /*
        Simple constraints object, for more advanced audio features see
        https://addpipe.com/blog/audio-constraints-getusermedia/
    */

    var constraints = { audio: true, video: false }

    /*
      Disable the record button until we get a success or fail from getUserMedia() 
  */

    recordButton.disabled = true;
    stopButton.disabled = false;
    pauseButton.disabled = false;
    stopAndDeleteButton.disabled = false;
    recordButton.className = "btn btn-lg btn-default";
    stopButton.className = "btn btn-lg btn-danger";
    stopAndDeleteButton.className = "btn btn-lg btn-danger";
    pauseButton.className = "btn btn-lg btn-warning";
    recordStatus.innerHTML = "<br/><span class='text text-success' >در حال ضبط صدا</span>";
    /*
        We're using the standard promise based getUserMedia() 
        https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/getUserMedia
    */

    navigator.mediaDevices.getUserMedia(constraints).then(function (stream) {
        console.log("getUserMedia() success, stream created, initializing Recorder.js ...");

        /*
            create an audio context after getUserMedia is called
            sampleRate might change after getUserMedia is called, like it does on macOS when recording through AirPods
            the sampleRate defaults to the one set in your OS for your playback device

        */
        audioContext = new AudioContext();

        /*  assign to gumStream for later use  */
        gumStream = stream;

        /* use the stream */
        input = audioContext.createMediaStreamSource(stream);

        /* 
            Create the Recorder object and configure to record mono sound (1 channel)
            Recording 2 channels  will double the file size
        */
        rec = new Recorder(input, { numChannels: 1 });

        //start the recording process
        rec.record();

        console.log("Recording started");

    }).catch(function (err) {
        //enable the record button if getUserMedia() fails
        recordButton.disabled = false;
        stopButton.disabled = true;
        pauseButton.disabled = true;
    });
}

function pauseRecording() {
    console.log("pauseButton clicked rec.recording=", rec.recording);
    if (rec.recording) {
        //pause
        rec.stop();
        pauseButton.innerHTML = "ادامه";
        recordStatus.innerHTML = "<br/><span class='text text-warning' >ضبط صدا متوقف شد</span>";
    } else {
        //resume
        rec.record();
        pauseButton.innerHTML = "توقف";
        recordStatus.innerHTML = "<br/><span class='text text-success' >در حال ضبط صدا</span>";

    }
}

function stopRecording() {
    console.log("stopButton clicked");

    //disable the stop button, enable the record too allow for new recordings
    stopButton.disabled = true;
    stopAndDeleteButton.disabled = true;
    recordButton.disabled = false;
    pauseButton.disabled = true;

    stopButton.className = "btn btn-lg btn-default";
    stopAndDeleteButton.className = "btn btn-lg btn-default";
    pauseButton.className = "btn btn-lg btn-default";
    recordButton.className = "btn btn-lg btn-success";

    //reset button just in case the recording is stopped while paused
    pauseButton.innerHTML = "توقف";
    recordStatus.innerHTML = "<br/><span class='text text-danger' >ضبط صدا به پایان رسید</span>";

    //tell the recorder to stop the recording
    rec.stop();

    //stop microphone access
    gumStream.getAudioTracks()[0].stop();

    //create the wav blob and pass it on to createDownloadLink
    rec.exportWAV(createDownloadLink);
}

function stopAndDeleteRecording() {
    console.log("stopButton clicked");

    //disable the stop button, enable the record too allow for new recordings
    stopButton.disabled = true;
    stopAndDeleteButton.disabled = true;
    recordButton.disabled = false;
    pauseButton.disabled = true;

    stopButton.className = "btn btn-lg btn-default";
    stopAndDeleteButton.className = "btn btn-lg btn-default";
    pauseButton.className = "btn btn-lg btn-default";
    recordButton.className = "btn btn-lg btn-success";

    //reset button just in case the recording is stopped while paused
    pauseButton.innerHTML = "توقف";
    recordStatus.innerHTML = "<br/><span class='text text-danger' >ضبط صدا به پایان رسید</span>";

    //tell the recorder to stop the recording
    rec.stop();

    //stop microphone access
    gumStream.getAudioTracks()[0].stop();

    //create the wav blob and pass it on to createDownloadLink
    rec.exportWAV(nothing);
}

function nothing() {

}
function createDownloadLink(blob) {

    var url = URL.createObjectURL(blob);
    var au = document.createElement('audio');
    var li = document.createElement('li');
    var link = document.createElement('a');
    var inputName = document.createElement('input');

    var row = document.createElement('div');
    row.className = "row";

    var col1 = document.createElement('div');
    col1.className = "col-md-8";


    var col2 = document.createElement('div');
    col2.className = "col-md-4";
    //name of .wav file to use during upload and download (without extendion)
    var filename = new Date().toISOString();

    //add controls to the <audio> element
    au.controls = true;
    au.src = url;

    //save to disk link
    link.href = url;
    link.download = filename + ".wav"; //download forces the browser to donwload the file using the  filename
    link.innerHTML = "دانلود";
    link.className = "btn btn-info";

    //add the new audio element to li
    col1.appendChild(au);

    //add the filename to the li
    // li.appendChild(document.createTextNode(filename + ".wav "));

    //add the save to disk link to li
    col2.appendChild(link);
    BlobFiles.push({ blob: blob, filename: filename + ".wav" });
    console.log(BlobFiles);

    recordStatus.innerHTML = "<br/><span class='text text-info' >درحال ارسال صدا به سرور</span>";

    var removeLink = document.createElement('a');
    removeLink.addEventListener("click",
        function (event) {
            var index = BlobFiles.indexOf({ blob: blob, filename: filename + ".wav" });
            BlobFiles.splice(index, 1);
            row.remove();
            $("#voiceControls").show();
        });
    removeLink.className = "btn btn-danger";
    removeLink.innerHTML = "حذف";
    col2.appendChild(removeLink);
    row.appendChild(col1);
    row.appendChild(col2);

    inputName.type = "hidden";


    var xhr = new XMLHttpRequest();
    xhr.progress = function (e) {
        var percentComplete = e.loaded / e.total * 100;
        console.log(percentComplete);
    };
    xhr.onload = function (e) {
        if (this.readyState === 4) {
            var response = JSON.parse(xhr.response);
            debugger;
            if (response.status === "Error") {
                swal('خطا', response.message, 'error');

                recordStatus.innerHTML = "<br/><span class='text text-danger' >" + response.message + "</span>";
            } else if (response.status === "Done") {
                $("#voiceControls").hide();
                inputName.value = response.data.fileName;
                inputName.name = "VoiceFileName";
                col2.appendChild(inputName);
                recordingsList.appendChild(row);
                recordStatus.innerHTML = "<br/><span class='text text-info' >فایل صوتی با موفقیت ذخیره شد</span>";
            }
        }
    };
    var fd = new FormData();
    fd.append("audio_data", blob, filename + ".wav");
    xhr.open("POST", "/up-audio", true);
    xhr.send(fd);
}

function RemoveDiv(elId) {
    $("#" + elId).remove();
    $("#voiceControls").show();
}