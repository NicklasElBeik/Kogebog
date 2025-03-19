import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/profile_provider.dart';
import '../providers/recipe_provider.dart';

class RecipeListView extends ConsumerWidget {
  const RecipeListView({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final userId = ref.watch(profileProvider);
    final recipeList = ref.watch(recipeProvider(userId ?? ''));

    return Scaffold(
      appBar: AppBar(title: Text("My Recipes")),
      body: recipeList.when(
        data: (recipes) => ListView.builder(
          itemCount: recipes.length,
          itemBuilder: (context, index) {
            return ListTile(
              title: Text(recipes[index].title),
              onTap: () => Navigator.pushNamed(context, '/recipe/${recipes[index].id}'),
            );
          },
        ),
        loading: () => Center(child: CircularProgressIndicator()),
        error: (_, __) => Center(child: Text("Failed to load recipes")),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => Navigator.pushNamed(context, '/recipe/new'),
        child: Icon(Icons.add),
      ),
    );
  }
}
