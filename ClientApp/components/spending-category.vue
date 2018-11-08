<template>
    <div>
        <h1>Spending Category</h1>
        <div v-for="cate in listSpendingCategories" :key="cate.id">
            <span>{{cate.categoryName}}</span>
            <button @click="editCategory(cate)">Edit</button>
            <button @click="deleteCategory(cate.id)">Delete</button>
        </div>
        <button @click="addNewCategory()">Add Category</button>
        <div v-if="spendingCategory">
            <div>
                <label>Category Name:</label>
                <input v-model="spendingCategory.categoryName"/>
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
      apiPath: `/api/category`,
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
