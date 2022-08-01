const app = Vue.createApp({
    data() {
        return {
            loading: false,
            musicians: [],
            selectedMusician: null,
            albumModel: { musicianId: 0, name: "", fileName: "placeholder.jpg" },
            updatedAlbum: null,
            selectedAlbumIndex: 0,
            file: null,
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

            this.uploadFile().then(() => {
                axios.post('/album', this.albumModel).
                    then(res => this.selectedMusician.albums.push(res.data)).
                    catch(error => console.log(error)).
                    then(() => {
                        this.loading = false;
                        this.albumModel.name = "";
                    })
            })
            
        },
        selectMusician(musician) {
            this.selectedMusician = musician;
            this.albumModel.musicianId = musician.id;
            this.updatedAlbum = null;
        },
        deleteAlbum(id, index) {
            this.loading = true;
            axios.delete('/album/' + id).
                then(res => this.selectedMusician.albums.splice(index, 1)).
                catch(error => console.log(error)).
                then(() => this.loading = false)

            axios.delete('/RemoveFile/Album/' + id).catch(error => console.log(error));
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
        },
        fileChange(event) {
            this.file = event.target.files[0];
        },
        uploadFile() {
            const formData = new FormData();
            formData.append('file', this.file);

            return axios.post('/upload/AlbumFile', formData, {
                headers: {
                    'Content-type': 'multipart/form-data'
                }
            }).then(res => {
                console.log(res);
                this.albumModel.fileName = res.data;
            })
        },
    },
}).mount('#app')