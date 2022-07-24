let app = Vue.createApp({
    data() {
        return {
            editing: false,
            loading: false,
            musicianIndex: 0,
            musicians: [],
            musicianModel: { id: 0, name: "", description: ""}
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
            axios.post('/musician', this.musicianModel).
                then(res =>
                {
                    this.musicians.push(res.data);
                    this.editing = false;
                }).catch(error => console.log(error)).
                then(() => this.loading = false)
        },
        newMusician() {
            this.editing = true;
            this.musicianModel.id = 0;
            this.musicianModel.name = "";
            this.musicianModel.description = "";
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
        }
    }
}).mount('#app')
