import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import 'package:kogebog_flutter/models/profile.dart';
import '../providers/profile_provider.dart';

class WelcomeView extends ConsumerWidget {
  const WelcomeView({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final profileId = ref.watch(profileProvider);
    TextEditingController nameController = TextEditingController();

    if (profileId != null) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        context.go('/recipes');
      });
      return Scaffold(
        body: Center(child: CircularProgressIndicator()),
      );
    }
    
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text("Enter Your Name"),
            TextField(controller: nameController),
            ElevatedButton(
              onPressed: () async {
                Profile newProfile = Profile(id: '', name: nameController.text);
                await ref.read(profileProvider.notifier).createProfile(newProfile);

                if (context.mounted) {
                  context.go('/recipes');
}
              },
              child: Text("Start"),
            ),
          ],
        ),
      ),
    );
  }
}
