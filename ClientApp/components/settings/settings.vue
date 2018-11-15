<template>
    <div>
        <h3>Settings Page</h3>
        <manage-icon></manage-icon>
        <manage-spending-account></manage-spending-account>
        <manage-spending-category></manage-spending-category>
        <manage-spending-change></manage-spending-change>
    </div>
</template>

<script>
import Icon from "./icon";
import SpendingAccount from "./spending-account";
import SpendingCategory from "./spending-category";
import SpendingChange from "./spending-change";

export default {
  components: {
    "manage-icon": Icon,
    "manage-spending-account": SpendingAccount,
    "manage-spending-category": SpendingCategory,
    "manage-spending-change": SpendingChange,
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
