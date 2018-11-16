<template>
  <div v-if="transaction" class="spending-item-container">
    <h4>{{transactionTitle}}</h4>
    <div>
        <label>Value Change:</label>
        <input type="number" v-model="transaction.changeValue"/>
    </div>
    <div>
        <label>Description:</label>
        <textarea v-model="transaction.description"/>
    </div>
    <div>
        <label>Transaction Type:</label>
        <div>                    
            <input type="radio" id="sxpense" value="0" v-model="transaction.type">
            <label for="one">Expense</label>           
            <input type="radio" id="income" value="1" v-model="transaction.type">
            <label for="two">Income</label>
        </div>
    </div>
    <div>
        <label>Category Type:</label>
        <select v-model="transaction.categoryID">
            <option v-for="cate in listSpendingCategories" :key="cate.id" :value="cate.id">{{cate.categoryName}}</option>
        </select>
    </div>
    <div>
        <label>Acccount:</label>
        <select v-model="transaction.spendingAccountID">
            <option v-for="acc in listSpendingAccounts" :key="acc.id" :value="acc.id">{{acc.accountName}}</option>
        </select>
    </div>
    <button type="button" class="btn btn-info" @click="save()">Save</button>
    <button type="button" class="btn btn-danger" v-if="isEdit" @click="deleteTransaction(transaction.id)">Delete</button>
    <button type="button" class="btn btn-light" @click="cancel()">Cancel</button>    
    
  </div> 
</template>

<script>
export default {
  props: ["isEdit", "transaction"],
  data: function() {
    return {
      apiPath: `/api/SpendingItem`,
      listSpendingCategories: [],
      listSpendingAccounts: []
    };
  },
  computed: {
    transactionTitle() {
      return (this.isEdit ? "Edit " : "New ") + this.transactionTypeName;
    },
    transactionTypeName() {
      if (this.transaction.type == 0) {
        return "Expense";
      }
      if (this.transaction.type == 1) {
        return "Income";
      }
      return "Transaction";
    }
  },
  methods: {
    async createTransaction() {
      let response = await this.$http.post(this.apiPath, this.transaction);
    },
    async updateTransaction() {
      let response = await this.$http.put(
        `${this.apiPath}/${this.transaction.id}`,
        this.transaction
      );
    },
    save() {
      if (this.isEdit) {
        this.updateTransaction().then(() => this.closeTransaction());
      } else {
        this.createTransaction().then(() => this.closeTransaction());
      }
    },
    cancel() {
      this.closeTransaction();
    },
    async deleteTransaction(id) {
      let response = await this.$http.delete(`${this.apiPath}/${id}`);
      this.closeTransaction();
    },
    closeTransaction() {
      this.$emit("close-transaction");
    },
    async loadCategories() {
      let response = await this.$http.get(`/api/Category`);
      this.listSpendingCategories = response.data;
    },
    async loadAccounts() {
      let response = await this.$http.get(`/api/SpendingAccount`);
      this.listSpendingAccounts = response.data;
    }
  },
  async created() {
    this.loadCategories();
    this.loadAccounts();
  }
};
</script>