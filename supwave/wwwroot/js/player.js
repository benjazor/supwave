// Retrive buttons from page
var buttons = {
    back: document.getElementById('btn-back'),
    backward: document.getElementById('btn-backward'),
    playpause: document.getElementById('btn-playpause'),
    forward: document.getElementById('btn-forward'),
    next: document.getElementById('btn-next'),
    mute: document.getElementById('btn-mute')
}

// Variables
var playlistSelect = document.getElementById('playlistId');
var isPlaying = false;

// Create the wave player
var wavesurfer = WaveSurfer.create({
    container: '#waveform',
    waveColor: '#7f8c8d',
    progressColor: '#2980b9',
    cursorColor: '#4353FF',
    barWidth: 5,
    barRadius: 2,
    cursorWidth: 0,
    height: 200,
    hideScrollbar: true,
    partialRender: true
});

// Play Pause function
function playpause() {
    if (isPlaying) { wavesurfer.pause(); }
    else { wavesurfer.play(); }
    buttons.forward.disabled = !buttons.forward.disabled;
    buttons.backward.disabled = !buttons.backward.disabled;
    isPlaying = !isPlaying;
}
// Play Pause Button
buttons.playpause.addEventListener("click", playpause);

// Mute toggle
buttons.mute.addEventListener("click", () => { wavesurfer.setMute( !wavesurfer.getMute() ) });

// Execute every time a song is loaded
wavesurfer.on('ready', function () {
    buttons.playpause.disabled = false;
    wavesurfer.zoom(50);
});

// Forward button
buttons.forward.addEventListener("click", function () { wavesurfer.skipForward(5); });

// Backward button
buttons.backward.addEventListener("click", function () { wavesurfer.skipBackward(5); });

// Function to load a song to the player
function loadToPlayer(path) {
    buttons.backward.disabled = true;
    buttons.forward.disabled = true;
    buttons.playpause.disabled = true;
    isPlaying = false;
    wavesurfer.load(path);
}

// Refresh song list when playlist is changed
playlistSelect.onchange = function () {
    // Retrive the playlist id
    var playlistId = this.value;

    songs = document.getElementById("songs");

    // EMPTY
    while (songs.firstChild) {
        songs.removeChild(songs.firstChild);
    }

    // LOAD AND ADD NEW SONGS
    fetch(`/get-playlist-songs?id=${playlistId}`, { method: 'GET' })
        .then(response => response.json())
        .then(result => {
            result.value.forEach(song => {

                var li = document.createElement("li");
                var p = document.createElement("p");
                var button = document.createElement("button");

                p.className = 'name';
                p.innerText = song.name;

                button.addEventListener("click", () => { loadToPlayer(song.path) });
                button.innerText = 'Load';

                li.appendChild(p);
                li.appendChild(button);

                songs.appendChild(li);
            });
        })
        .catch(error => console.log('error', error));
}