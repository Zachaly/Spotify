function addSong(songId, playlistId) {
    axios.post("/Playlist/AddSong/" + playlistId + "/" + songId).
        catch(error => console.log(error));
}

function removeSong(songId, playlistId) {
    axios.delete("/Playlist/RemoveSong/" + playlistId + "/" + songId).
        catch(error => console.log(error));
}