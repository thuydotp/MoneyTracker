<template>
    <div>
        <h1>Manage Icons</h1>
        <div v-for="icon in iconList" :key="icon.id">
            <span>{{icon.iconName}}</span>
            <button @click="editIcon(icon)">Edit</button>
            <button @click="deleteIcon(icon.id)">Delete</button>
        </div>
        <button @click="addNewIcon()">Add Icon</button>
        <div v-if="icon">
            <div>
                <label>Icon Name:</label>
                <input v-model="icon.iconName"/>
            </div>
            <button @click="save()">Save</button>
            <button @click="cancel()">Cancel</button>
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {
      apiPath: `/api/icon`,
      iconList: null,
      icon: null,
      isEditMode: false
    };
  },

  methods: {
    async loadIcons() {
      let response = await this.$http.get(this.apiPath);
      this.iconList = response.data;
    },

    addNewIcon() {
      this.icon = {};
    },

    async createIcon() {
      let response = await this.$http.post(this.apiPath, this.icon);
      this.loadIcons();
      this.icon = null;
      this.isEditMode = false;
    },
    async updateIcon() {
      let response = await this.$http.put(
        `${this.apiPath}/${this.icon.id}`,
        this.icon
      );
      this.loadIcons();
      this.icon = null;
      this.isEditMode = false;
    },
    save() {
      if (this.isEditMode) {
        this.updateIcon();
      } else {
        this.createIcon();
      }
    },
    cancel() {
      this.icon = null;
      this.isEditMode = false;
    },
    editIcon(icon) {
      this.isEditMode = true;
      this.icon = Object.assign({}, icon);
    },
    async deleteIcon(id) {
      let response = await this.$http.delete(`${this.apiPath}/${id}`);
      this.loadIcons();
    }
  },

  async created() {
    this.loadIcons();
  }
};
</script>