function playSong(fileName, id) {
    updateSongInfo(id);

    let audio = document.getElementById('audio');
    audio.src = `/Songs/${fileName}`;
    audio.play();

    axios.post('/AddPlay/' + id).catch(error => console.log(error));
}

function updateSongInfo(id) {
    let creator = document.getElementById(`song-${id}-creator`).innerText;
    let song = document.getElementById(`song-${id}-name`).innerText;

    document.getElementById('audio-song-name').innerText = song;
    document.getElementById('audio-song-creator').innerText = creator;
}