const app = Vue.createApp({
    data() {
        return {
            editing: false,
            loading: false,
            albums: [],
            selectedAlbum: null,
            songModel: { name: "", albumId: 0, creatorId: 0, fileName: "" },
            updatedSong: null,
            songIndex: 0,
            file: null,
        }
    },
    mounted() {
        this.getSongs();
    },
    methods: {
        getSongs() {
            this.loading = true;
            axios.get('/manager/song').
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

            this.uploadFile().then(() => {
                axios.post('/manager/song', this.songModel).
                    then(res => this.selectedAlbum.songs.push(res.data)).
                    catch(error => console.log(error)).
                    then(() => {
                        this.loading = false;
                        this.songModel.name = "";
                    })
            });
        },
        uploadFile() {
            const formData = new FormData();
            formData.append('file', this.file);

            return axios.post('/upload/SongFile', formData, {
                headers: {
                    'Content-type': 'multipart/form-data'
                }
            }).then(res => {
                console.log(res);
                this.songModel.fileName = res.data;
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
            axios.put('/manager/song', this.updatedSong).
                then(res => this.selectedAlbum.songs.splice(this.songIndex, 1, res.data)).
                catch(error => console.log(error)).
                then(() => {
                    this.loading = false;
                    this.updatedSong = null;
                })
        },
        deleteSong(id, index) {
            this.loading = false;
            axios.delete('/manager/song/' + id).
                then(res => this.selectedAlbum.songs.splice(index, 1)).
                catch(error => console.log(error)).
                then(() => this.loading = false)

            axios.delete('/RemoveFile/Song/' + id).catch(error => console.log(error));
        },
        fileChange(event) {
            this.file = event.target.files[0];
        }
    }
}).mount('#app')