function likeAlbum(id) {
    axios.post('/like/LikeAlbum/' + id).then(() => {
        likeButton = document.getElementById('like-button');
        likeButton.textContent = 'Unlike';

        likeButton.onclick = () => unLikeAlbum(id);

        likeButton.classList.remove('is-success');
        likeButton.classList.add('is-warning');
    })
}

function unLikeAlbum(id) {
    axios.post('/like/LikeAlbum/' + id).then(() => {
        likeButton = document.getElementById('like-button');
        likeButton.textContent = 'Like';

        likeButton.onclick = () => likeAlbum(id);

        likeButton.classList.remove('is-warning');
        likeButton.classList.add('is-success');
    })
}