function playSong(index) {
    let audio = document.getElementById('audio');
    let song = document.getElementById(`song-play-${index}`);

    let doesSongExist = song !== null;

    if (!doesSongExist) {
        song = document.getElementById('song-play-1')
    }

    let src = song.dataset.filename;
    let id = song.dataset.id;

    updateSongInfo(id);

    audio.src = `/Songs/${src}`;

    audio.onended = () => {
        playSong(++index);
    }

    if (doesSongExist) {
        audio.play();
        axios.post('/AddPlay/' + id).catch(error => console.log(error));
    }
}

function updateSongInfo(id) {
    let creator = document.getElementById(`song-${id}-creator`).innerText;
    let song = document.getElementById(`song-${id}-name`).innerText;

    document.getElementById('audio-song-name').innerText = song;
    document.getElementById('audio-song-creator').innerText = creator;
}