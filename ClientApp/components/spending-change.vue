<template>
    <div>
        <h3>Create new {{spendingChangeTitle}}</h3>
        <div class="spending-item-container">
            <div>
                <label>Value Change:</label>
                <input type="number" :model="spendingItem.valueChange"/>
            </div>
            <div>
                <label>Description:</label>
                <textarea :model="spendingItem.description"/>
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
                <label>Category Type: {{spendingItem.selectedCategoryType}}</label>
                <select v-model="spendingItem.selectedCategoryType">
                    <option value="cate1">Category 1</option>
                    <option value="cate1">Category 2</option>
                    <option value="cate1">Category 3</option>
                    <option value="cate1">Category 4</option>
                </select>
            </div>
        </div>
    </div>
</template>

<script>
export default {
  data: function() {
    return {
      spendingType: 0, //input
      spendingItem: {
        type: 0,
        valueChange: 10,
        description: "This is spending description",
        selectedCategoryType: ""
      }
    };
  },
  computed: {
    spendingChangeTitle() {
      return this.spendingItem.type == 0 ? "Expense" : "Income";
    }
  },
  methods: {
    addSpendingItem() {}
  }
};
</script>
