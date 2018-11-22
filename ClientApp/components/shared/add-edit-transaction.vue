<template>
    <div v-if="transaction" class="spending-item-container">
        <h4>{{transactionTitle}}</h4>
        <div>
            <p v-if="errors.length">
                <b>Please correct the following error(s):</b>
                <ul>
                    <li v-for="error in errors" :key="error">{{ error }}</li>
                </ul>
            </p>            
            <div>
                <label>Acccount:</label>
                <select v-model="transaction.spendingAccountID">
                    <option v-for="acc in listSpendingAccounts" :key="acc.id" :value="acc.id">{{acc.accountName}}</option>
                </select>
            </div>
            <div>
                <label>Value: </label>
                <input type="number" v-model="transaction.changeValue" />
            </div>
            <div>
                <label>Note: </label>
                <textarea v-model="transaction.description" />
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
                    <option v-for="cate in selectableCategories" :key="cate.id" :value="cate.id">{{cate.categoryName}}</option>
                </select>
            </div>
            <button type="submit" class="btn btn-info" @click="save($event)">Save</button>
            <button type="button" class="btn btn-danger" v-if="isEdit" @click="deleteTransaction(transaction.id)">Delete</button>
            <button type="button" class="btn btn-light" @click="cancel()">Cancel</button>    
      
        </div>
    </div> 
</template>

<script>
import { TransactionService } from '../../store/api.js'

export default {
  props: ["isEdit", "transaction"],
  data: function() {
    return {
      apiPath: `/api/Transaction`,
      listSpendingCategories: [],
      listSpendingAccounts: [],
      errors: []
    };
  },
  computed: {
    transactionTitle() {
      return (this.isEdit ? "Edit " : "New ") + this.displayedTransactionType;
    },
    displayedTransactionType() {
      return TransactionService.getDisplayedType(this.transaction.type);
    },
    selectableCategories(){
      let categories = this.listSpendingCategories || [];
      if(this.transaction.type !== undefined && this.transaction.type !== null){
        categories = categories.filter(x => x.type == this.transaction.type);
      }
      return categories;
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
    save(event) {
      if(!this.validateForm(event)){ return;}

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
    validateForm: function (e) {
      this.errors = [];

      if (!this.transaction.description) {
        this.errors.push('Note required.');
      }
      if (!this.transaction.changeValue) {
        this.errors.push('Change value required.');
      }
      if (!this.transaction.spendingAccountID) {
        this.errors.push('Account required.');
      }
      if (this.transaction.type === null || this.transaction.type === undefined) {
        this.errors.push('Transaction type required.');
      }
      if (!this.transaction.categoryID === null || this.transaction.categoryID === undefined) {
        this.errors.push('Category required.');
      }

      if(this.errors.length > 0){
        e.preventDefault();
        return;
      }
      return true;
    }
  },
  async created() {
    TransactionService.loadCategories().then((data) => this.listSpendingCategories = data);
    TransactionService.loadAccounts().then((data) => this.listSpendingAccounts = data);
  }
};
</script>