<template>
    <div>
        <h1>Manage Icons</h1>
        <button type="button" class="btn btn-success" @click="addNewIcon()">Create</button>
        <div v-if="icon">
            <div class="form-group">
              <label for="iconName">Icon Name</label>
              <input type="text" class="form-control" id="iconName" placeholder="Insert icon name here..." v-model="icon.iconName">
            </div>
            <button type="button" class="btn btn-info" @click="save()">Save</button>
            <button type="button" class="btn btn-light" @click="cancel()">Cancel</button>
        </div>
        <table class="table">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Icon Name</th>
              <th scope="col">Action</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(icon, index) in iconList" :key="icon.id">
              <th scope="row">{{index}}</th>
              <td>{{icon.iconName}}</td>
              <td>
                <button type="button" class="btn btn-primary" @click="editIcon(icon)">Edit</button>
                <button type="button" class="btn btn-danger" @click="deleteIcon(icon.id)">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
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