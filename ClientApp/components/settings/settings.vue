<template>
    <div>
        <h3>Settings Page</h3>
        <manage-icon></manage-icon>
        <manage-spending-account></manage-spending-account>
        <manage-spending-category></manage-spending-category>
        <manage-transaction></manage-transaction>
    </div>
</template>

<script>
import ManageIcon from "./manage-icon";
import ManageSpendingAccount from "./manage-spending-account";
import ManageSpendingCategory from "./manage-spending-category";
import ManageTransaction from "./manage-transaction";

export default {
  components: {
    "manage-icon": ManageIcon,
    "manage-spending-account": ManageSpendingAccount,
    "manage-spending-category": ManageSpendingCategory,
    "manage-transaction": ManageTransaction,
  },
  data: function(){
    return {
      showSummary: true,
      hideSummary: false
    }
  },
  methods: {
    toggleShowSummary() {
      this.showSummary = !this.showSummary;
    }
  }
};
</script>
