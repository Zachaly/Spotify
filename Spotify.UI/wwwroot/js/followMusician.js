function followMusician(id) {
    axios.post('/like/FollowMusician/' + id).then(() => {
        followButton = document.getElementById('follow-button');
        followButton.textContent = 'Stop following'

        followButton.onclick = () => unFollowMusician(id);

        followButton.classList.remove('is-success');
        followButton.classList.add('is-warning');
    }).catch(error => console.log(error));
}

function unFollowMusician(id) {
    axios.post('/like/FollowMusician/' + id).then(() => {
        followButton = document.getElementById('follow-button');
        followButton.textContent = 'Follow'

        followButton.onclick = () => followMusician(id);

        followButton.classList.remove('is-warning');
        followButton.classList.add('is-success');
    }).catch(error => console.log(error));
}