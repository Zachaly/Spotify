let app = Vue.createApp({
    data() {
        return {
            editing: false,
            loading: false,
            musicianIndex: 0,
            musicians: [],
            musicianModel: { id: 0, name: "", description: "", fileName: "placeholder.jpg" },
            file: null,
        }
    },
    mounted() {
        this.getMusicians();
    },
    methods: {
        getMusicians() {
            this.loading = true;
            axios.get('/musician').
                then(res => this.musicians = res.data).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        getMusicianForEdit(id) {
            this.loading = true;
            axios.get('/musician/' + id).
                then(res => {
                    console.log(res);
                    this.musicianModel = {
                        id: res.data.id,
                        name: res.data.name,
                        description: res.data.description
                    }
                }).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        addMusician() {
            this.loading = true;
            this.uploadFile().then(() => {
                axios.post('/musician', this.musicianModel).
                    then(res => {
                        this.musicians.push(res.data);
                        this.editing = false;
                    }).catch(error => console.log(error)).
                    then(() => this.loading = false)
            })
        },
        newMusician() {
            this.editing = true;
            this.musicianModel.id = 0;
            this.musicianModel.name = "";
            this.musicianModel.description = "";
            this.musicianModel.fileName = "placeholder.jpg"
        },
        editMusician(musician, index) {
            this.musicianIndex = index;
            this.getMusicianForEdit(musician.id);
            this.editing = true;
        },
        updateMusician() {
            this.loading = true;
            axios.put("/musician", this.musicianModel).
                then(res => {
                    this.editing = false;
                    this.musicians.splice(this.musicianIndex, 1, res.data)
                }).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        deleteMusician(id, index) {
            axios.delete("/musician/" + id).
                then(res => this.musicians.splice(index, 1)).
                catch(error => console.log(error)).
                then(() => this.loading = false);

            axios.delete('/RemoveFile/Musician/' + id).catch(error => console.log(error));
        },
        fileChange(event) {
            this.file = event.target.files[0];
        },
        uploadFile() {
            const formData = new FormData();
            formData.append('file', this.file);

            return axios.post('/upload/MusicianFile', formData, {
                headers: {
                    'Content-type': 'multipart/form-data'
                }
            }).then(res => {
                console.log(res);
                this.musicianModel.fileName = res.data;
            })
        },
    }
}).mount('#app')
