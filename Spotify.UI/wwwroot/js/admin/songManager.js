const app = Vue.createApp({
    data() {
        return {
            editing: false,
            loading: false,
            albums: [],
            selectedAlbum: null,
            songModel: { name: "", albumId: 0, creatorId: 0 },
            updatedSong: null,
            songIndex: 0
        }
    },
    mounted() {
        this.getSongs();
    },
    methods: {
        getSongs() {
            this.loading = true;
            axios.get('/song').
                then(res => this.albums = res.data).
                catch(error => console.log(error)).
                then(() => this.loading = false)
        },
        selectAlbum(album) {
            this.selectedAlbum = album;
            this.songModel.albumId = album.id;
            this.songModel.creatorId = album.creatorId;
            this.updatedSong = null;
        },
        addSong() {
            this.loading = true;
            axios.post('/song', this.songModel).
                then(res => this.selectedAlbum.songs.push(res.data)).
                catch(error => console.log(error)).
                then(() => {
                    this.loading = false;
                    this.songModel.name = "";
                })
        },
        selectSong(song, index) {
            this.updatedSong = {
                id: song.id,
                name: song.name
            }
            this.songIndex = index;
        },
        updateSong() {
            this.loading = false;
            axios.put('/song', this.updatedSong).
                then(res => this.selectedAlbum.songs.splice(this.songIndex, 1, res.data)).
                catch(error => console.log(error)).
                then(() => {
                    this.loading = false;
                    this.updatedSong = null;
                })
        },
        deleteSong(id, index) {
            this.loading = false;
            axios.delete('/song/' + id).
                then(res => this.selectedAlbum.songs.splice(index, 1)).
                catch(error => console.log(error)).
                then(() => this.loading = false)
        }
    }
}).mount('#app')