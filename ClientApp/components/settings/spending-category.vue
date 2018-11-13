<template>
    <div>
        <h1>Manage Spending Category</h1>
        <button type="button" class="btn btn-success" @click="addNewCategory()">Create</button>
        <div v-if="spendingCategory">
            <div class="form-group">
              <label for="categoryName">Category Name</label>
              <input type="text" class="form-control" id="categoryName" placeholder="Insert category name here..." v-model="spendingCategory.categoryName">
            </div>
            <button type="button" class="btn btn-info" @click="save()">Save</button>
            <button type="button" class="btn btn-light" @click="cancel()">Cancel</button>
        </div>
        <table class="table">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Category Name</th>
              <th scope="col">Action</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(cate, index) in listSpendingCategories" :key="cate.id">
              <th scope="row">{{index}}</th>
              <td>{{cate.categoryName}}</td>
              <td>
                <button type="button" class="btn btn-primary" @click="editCategory(cate)">Edit</button>
                <button type="button" class="btn btn-danger" @click="deleteCategory(cate.id)">Delete</button>
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
      apiPath: `/api/Category`,
      listSpendingCategories: null,
      spendingCategory: null,
      isEditMode: false
    };
  },

  methods: {
    async loadCategories() {
      let response = await this.$http.get(this.apiPath);
      this.listSpendingCategories = response.data;
    },

    addNewCategory() {
      this.spendingCategory = {};
    },

    async createCategory() {
      let response = await this.$http.post(this.apiPath, this.spendingCategory);
      this.loadCategories();
      this.spendingCategory = null;
      this.isEditMode = false;
    },
    async updateCategory() {
      let response = await this.$http.put(
        `${this.apiPath}/${this.spendingCategory.id}`,
        this.spendingCategory
      );
      this.loadCategories();
      this.spendingCategory = null;
      this.isEditMode = false;
    },
    save() {
      if (this.isEditMode) {
        this.updateCategory();
      } else {
        this.createCategory();
      }
    },
    cancel() {
      this.spendingCategory = null;
      this.isEditMode = false;
    },
    editCategory(category) {
      this.isEditMode = true;
      this.spendingCategory = Object.assign({}, category);
    },
    async deleteCategory(id) {
      let response = await this.$http.delete(`${this.apiPath}/${id}`);
      this.loadCategories();
    }
  },

  async created() {
    this.loadCategories();
  }
};
</script>
