const app = Vue.createApp({
    data() {
        return {
            loading: false,
            musicians: [],
            selectedMusician: null,
            albumModel: { musicianId: 0, name: "" },
            updatedAlbum: null,
            selectedAlbumIndex: 0,
        }
    },
    mounted() {
        this.getAlbums();
    },
    methods: {
        getAlbums() {
            this.loading = true;
            axios.get('/album').
                then(res => this.musicians = res.data).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        addAlbum() {
            this.loading = true;
            axios.post('/album', this.albumModel).
                then(res => this.selectedMusician.albums.push(res.data)).
                catch(error => console.log(error)).
                then(() => {
                    this.loading = false;
                    this.albumModel.name = "";
                })
        },
        selectMusician(musician) {
            this.selectedMusician = musician;
            this.albumModel.musicianId = musician.id;
        },
        deleteAlbum(id, index) {
            this.loading = true;
            axios.delete('/album/' + id).
                then(res => this.selectedMusician.albums.splice(index, 1)).
                catch(error => console.log(error)).
                then(() => this.loading = false)
        },
        selectAlbum(album, index) {
            this.updatedAlbum = {
                albumId: album.id,
                name: album.name
            };
            this.selectedAlbumIndex = index;
        },
        updateAlbum() {
            this.loading = true;
            axios.put('/album', this.updatedAlbum).
                then(res => this.selectedMusician.albums.splice(this.selectedAlbumIndex, 1, res.data)).
                catch(error => console.log(error)).
                then(() => {
                    this.loading = false;
                    this.updatedAlbum = null
                })
        }
    },
}).mount('#app')