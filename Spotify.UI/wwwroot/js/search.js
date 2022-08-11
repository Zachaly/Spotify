const app = Vue.createApp({
    data() {
        return {
            users: [],
            musicians: [],
            albums: [],
            songs: [],
            playlists: [],
            search: "",
        }
    },
    methods: {
        getSearchInfo(search) {
            axios.get("/search/musicians/" + search).
                then(res =>this.musicians = res.data).
                catch(error => console.log(error));

            axios.get("/search/albums/" + search).
                then(res => this.albums = res.data).
                catch(error => console.log(error));

            axios.get("/search/songs/" + search).
                then(res => this.songs = res.data).
                catch(error => console.log(error));

            axios.get("/search/users/" + search).
                then(res => this.users = res.data).
                catch(error => console.log(error));

            axios.get("/search/playlists/" + search).
                then(res => this.playlists = res.data).
                catch(error => console.log(error));
        },
    },
    watch: {
        search() {
            if (this.search.length < 3) {
                this.users = [];
                this.musicians = [];
                this.albums = [];
                this.songs = [];
                this.playlists = [];

            } else {
                this.getSearchInfo(this.search.replace(" ", "%20"));
            }
        }
    }
}).mount("#app");