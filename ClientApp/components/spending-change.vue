<template>
    <div>
        <h3>Create new {{spendingChangeTitle}}</h3>
        <button type="button" class="btn btn-success" @click="addNewSpendingItem()">Create</button>
        <div v-if="spendingItem" class="spending-item-container">
            <div>
                <label>Value Change:</label>
                <input type="number" v-model="spendingItem.changeValue"/>
            </div>
            <div>
                <label>Description:</label>
                <textarea v-model="spendingItem.description"/>
            </div>
            <div>
                <label>Spending Type:</label>
                <div>                    
                    <input type="radio" id="sxpense" value="0" v-model="spendingItem.type">
                    <label for="one">Expense</label>           
                    <input type="radio" id="income" value="1" v-model="spendingItem.type">
                    <label for="two">Income</label>
                </div>
            </div>
            <div>
                <label>Category Type:</label>
                <select v-model="spendingItem.categoryID">
                    <option v-for="cate in listSpendingCategories" :key="cate.id" :value="cate.id">{{cate.categoryName}}</option>
                </select>
            </div>
            <div>
                <label>Acccount:</label>
                <select v-model="spendingItem.spendingAccountID">
                    <option v-for="acc in listSpendingAccounts" :key="acc.id" :value="acc.id">{{acc.accountName}}</option>
                </select>
            </div>
            <button type="button" class="btn btn-info" @click="save()">Save</button>
            <button type="button" class="btn btn-light" @click="cancel()">Cancel</button>
        </div>
        
        <table class="table">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Category Type</th>
              <th scope="col">ChangeValue</th>
              <th scope="col">Description</th>
              <th scope="col">Category</th>
              <th scope="col">Spending Account</th>
              <th scope="col">Action</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(spendingItem, index) in listSpendingItems" :key="spendingItem.id">
              <th scope="row">{{index}}</th>
              <td>{{spendingItem.type}}</td>
              <td>{{spendingItem.changeValue}}</td>
              <td>{{spendingItem.description}}</td>
              <td>{{getCategoryName(spendingItem.categoryID)}}</td>
              <td>{{getAccountName(spendingItem.spendingAccountID)}}</td>
              <td>
                <button type="button" class="btn btn-primary" @click="editSpendingItem(spendingItem)">Edit</button>
                <button type="button" class="btn btn-danger" @click="deleteSpendingItem(spendingItem.id)">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
    </div>
</template>

<script>
export default {
  data: function() {
    return {
      apiPath: `/api/SpendingItem`,
      isEditMode: false,
      listSpendingItems: [],
      spendingItem: null,
      listSpendingCategories: [],
      listSpendingAccounts: []
    };
  },
  computed: {
    spendingChangeTitle() {
      if (this.spendingItem) {
        if (this.spendingItem.type == 0) {
          return "Expense";
        }
        if (this.spendingItem.type == 1) {
          return "Income";
        }
      }
      return "Spending Item";
    }
  },
  methods: {
    async loadSpendingItems() {
      let response = await this.$http.get(this.apiPath);
      this.listSpendingItems = response.data;
    },
    addNewSpendingItem() {
      this.spendingItem = {};
    },
    async createSpendingItem() {
      let response = await this.$http.post(this.apiPath, this.spendingItem);
      this.loadSpendingItems();
      this.spendingItem = null;
      this.isEditMode = false;
    },
    async updateSpendingItem() {
      let response = await this.$http.put(
        `${this.apiPath}/${this.spendingItem.id}`,
        this.spendingItem
      );
      this.loadSpendingItems();
      this.spendingItem = null;
      this.isEditMode = false;
    },
    save() {
      if (this.isEditMode) {
        this.updateSpendingItem();
      } else {
        this.createSpendingItem();
      }
    },
    cancel() {
      this.spendingItem = null;
      this.isEditMode = false;
    },
    editSpendingItem(spendingItem) {
      this.isEditMode = true;
      this.spendingItem = Object.assign({}, spendingItem);
    },
    async deleteSpendingItem(id) {
      let response = await this.$http.delete(`${this.apiPath}/${id}`);
      this.loadSpendingItems();
    },
    //
    
    async loadCategories() {
      let response = await this.$http.get(`/api/Category`);
      this.listSpendingCategories = response.data;
    },
    async loadAccounts() {
      let response = await this.$http.get(`/api/SpendingAccount`);
      this.listSpendingAccounts = response.data;
    },
    getCategoryName(cateID){
      return this.listSpendingCategories.find(x => x.id === cateID).categoryName;
    },
    getAccountName(accountID){
      return this.listSpendingAccounts.find(x => x.id === accountID).accountName;
    }
  },

  async created() {
    this.loadSpendingItems();
    this.loadCategories();
    this.loadAccounts();
  }
};
</script>
