<template>
    <div>      
        <h1>Transaction Page</h1>
        <transaction-summary v-if="showSummary" @create-transaction="createTransaction"></transaction-summary>
        <manage-transactions v-if="showSummary"></manage-transactions>
        <add-edit-transaction v-if="editedTransaction" 
                            :isEdit="isEditTransaction" :transaction="editedTransaction" 
                            @close-transaction="closeTransaction()"></add-edit-transaction>
    </div>
</template>

<script>
import SpendingSummary from "./spending-summary";
import SpendingChange from "./spending-change";
import AddEditTransaction from "./add-edit-transaction";

export default {
  components: {
    "transaction-summary": SpendingSummary,
    "manage-transactions": SpendingChange,
    "add-edit-transaction": AddEditTransaction
  },
  data: function() {
    return {
      showSummary: true,
      editedTransaction: null,
      isEditTransaction: false,
      transactionType: null
    };
  },
  methods: {
    createTransaction(transactionType) {
      this.isEditTransaction = false;
      this.editedTransaction = { type: transactionType };
      this.showSummary = false;
    },
    editTransaction(transaction) {
      this.isEditTransaction = true;
      this.editedTransaction = Object.assign({}, transaction);
      this.showSummary = false;
    },
    closeTransaction() {
      this.editedTransaction = null;
      this.showSummary = true;
    }
  }
};
</script>
