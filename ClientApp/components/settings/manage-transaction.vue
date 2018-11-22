<template>
    <div>
        <h3>Manage Transaction</h3>
        <button type="button" class="btn btn-success" @click="addNewTransaction()">Create</button>
        <add-edit-transaction v-if="editedTransaction" 
                            :isEdit="isEditTransaction" :transaction="editedTransaction" 
                            @close-transaction="closeTransaction()"></add-edit-transaction>
        <transaction-list :transactions="transactions" 
                          @edit-transaction="editTransaction"
                          @delete-transaction="deleteTransaction"></transaction-list>
    </div>
</template>

<script>
import TransactionList from "../shared/transaction-list";
import AddEditTransaction from "../shared/add-edit-transaction";

export default {
  components: {
    'transaction-list': TransactionList,
    'add-edit-transaction': AddEditTransaction
  },
  data: function() {
    return {
      apiPath: `/api/Transaction`,
      isEditTransaction: false,
      transactions: [],
      editedTransaction: null,
      listSpendingCategories: [],
      listSpendingAccounts: []
    };
  },
  computed: {
  },
  methods: {
    async loadTransactions() {
      let response = await this.$http.get(this.apiPath);
      this.transactions = response.data;
    },
    addNewTransaction() {
      this.editedTransaction = {};
    },
    async createTransaction() {
      let response = await this.$http.post(this.apiPath, this.editedTransaction);
      this.loadTransactions();
      this.editedTransaction = null;
      this.isEditTransaction = false;
    },
    async updateTransaction() {
      let response = await this.$http.put(
        `${this.apiPath}/${this.editedTransaction.id}`,
        this.transaction
      );
      this.loadTransactions();
      this.editedTransaction = null;
      this.isEditTransaction = false;
    },
    save() {
      if (this.isEditTransaction) {
        this.updateTransaction();
      } else {
        this.createTransaction();
      }
    },
    cancel() {
      this.editedTransaction = null;
      this.isEditTransaction = false;
    },
    editTransaction(transaction) {
      this.isEditTransaction = true;
      this.editedTransaction = Object.assign({}, transaction);
    },
    async deleteTransaction(id) {
      let response = await this.$http.delete(`${this.apiPath}/${id}`);
      this.loadTransactions();
    },
    closeTransaction() {      
      this.loadTransactions();
      this.editedTransaction = null;
      this.showSummary = true;
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
    this.loadTransactions();
    this.loadCategories();
    this.loadAccounts();
  }
};
</script>
