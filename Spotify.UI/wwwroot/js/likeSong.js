function likeSong(songId) {
    axios.post('/like/LikeSong/' + songId).then(() => {
        songButton = document.getElementById('song-' + songId);
        songButton.textContent = "Unlike";

        songButton.onclick = () => unLikeSong(songId);

        songButton.classList.remove('is-success');
        songButton.classList.add('is-warning');
    }).catch(error => console.log(error));
}

function unLikeSong(songId) {
    axios.post('/like/LikeSong/' + songId).then(() => {
        songButton = document.getElementById('song-' + songId);
        songButton.textContent = "Like";

        songButton.onclick = () => likeSong(songId);

        songButton.classList.remove('is-warning');
        songButton.classList.add('is-success');
    }).catch(error => console.log(error));
}